using Marten;
using PatientMs.Consumer;
using PatientMs.Data;
using PatientMs.Service;
using PatientMs.Service.Client;
using PatientMs.Service.Publisher;
using Microsoft.Extensions.DependencyInjection;

namespace PatientMs
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var Configuration = configBuilder.Build();

            // Add services to the container.
            builder.Services.AddControllers();


            #region Database

            builder.Services.AddMarten(options =>
            {

                if (Environment.GetEnvironmentVariable("POSTGRES_CONNECTIONSTRING") == null) { Console.WriteLine("No environment connection string found. falling back to configuration value."); }
                options.Connection(Environment.GetEnvironmentVariable("POSTGRES_CONNECTIONSTRING") ?? Configuration.GetValue<string>("connectionStrings:postgres") ?? throw new ArgumentNullException("No postgres connection string!"));
                options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All; //(de docs raden aan om dit in een IsDevEnv wrapper te doen, maar gaan we dit echt in prod zetten?)
                options.UseDefaultSerialization(nonPublicMembersStorage: NonPublicMembersStorage.All); //wacky workaround voor value-objects, zonder deze probeert marten
                                                                                                       //de normale constructors te gebruiken -> exception want hij snapt de value-objects
                                                                                                       //niet
            });


            #endregion

            #region services

            //services
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IInsuranceService, InsuranceService>();
            builder.Services.AddScoped<IMedicalConditionService, MedicalConditionService>();
            builder.Services.AddScoped<ICostService, CostService>();

            //clients
            builder.Services.AddScoped<ICommunicationClient, CommunicationClient>();
            builder.Services.AddScoped<ITreatmentClient, TreatmentClient>();

            //accessors
            builder.Services.AddScoped<IPatientAccessor, MartenPatientAccessor>();
            builder.Services.AddScoped<ICostAccessor, CostAccessor>();


            #endregion

            #region HTTP clients

            builder.Services.AddHttpClient("treatment", options =>
            {
                if (Environment.GetEnvironmentVariable("TREATMENTSERVICE_ADRESS") == null) { Console.WriteLine("No Medical service adress found in the environment. falling back to configuration"); }

                options.BaseAddress = new Uri(Environment.GetEnvironmentVariable("TREATMENTSERVICE_ADRESS") ?? Configuration.GetValue<string>("TreatmentService:Adress") ?? throw new ArgumentNullException("TreatmentService has no adress set. set the adress in appsettings.json or in the TREATMENTSERVICE_ADRESS environment variable"));
            });

            builder.Services.AddHttpClient("communication", options =>
            {
                if (Environment.GetEnvironmentVariable("COMMUNICATIONSERVICE_ADRESS") == null) { Console.WriteLine("No Communication service adress found in the environment. falling back to configuration"); }

                options.BaseAddress = new Uri(Environment.GetEnvironmentVariable("COMMUNICATIONSERVICE_ADRESS") ?? Configuration.GetValue<string>("CommunicationService:Adress") ?? throw new ArgumentNullException("CommunicationService has no adress set. set the adress in appsettings.json or in the COMMUNICATIONSERVICE_ADRESS environment variable"));
            });

            #endregion

            #region RabbitMQ

            RabbitMQ.Client.ConnectionFactory MQConnectionFactory = new RabbitMQ.Client.ConnectionFactory
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOSTNAME") ?? Configuration.GetValue<string>("RabbitMQ:Hostname"),
                UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? Configuration.GetValue<string>("RabbitMQ:Username"),
                Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? Configuration.GetValue<string>("RabbitMQ:Password"),
                Port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT") ?? Configuration.GetValue<string>("RabbitMQ:Port") ?? "5432")
            };

            builder.Services.AddSingleton(MQConnectionFactory);

            //publishers
            builder.Services.AddScoped<IMedicalConditionPublisher, MedicalConditionPublisher>();

            //consumers
            builder.Services.AddScoped<InsuranceConsumer>();
            builder.Services.AddScoped<MedicalConditionConsumer>();




            #endregion

            // swagger setup
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure exception handling

            app.UseExceptionHandler(ErrorHandler.MapExceptions);

            //initialize listeners
            using (var scope = app.Services.CreateScope())
            {
                var insuranceConsumer = scope.ServiceProvider.GetRequiredService<InsuranceConsumer>();
                var medicalConditionConsumer = scope.ServiceProvider.GetRequiredService<MedicalConditionConsumer>();

                insuranceConsumer.Initialize();
                medicalConditionConsumer.Initialize();
            }



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}