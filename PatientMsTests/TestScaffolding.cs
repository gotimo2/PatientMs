using Marten;
using Microsoft.Extensions.Configuration;

namespace PatientMsTests
{
    class TestScaffolding
    {
        private static DocumentStore? store;


        public static IConfiguration GetConfiguration()
        {
            ConfigurationBuilder builder = new();
            builder.AddJsonFile("./appsettings.json");
            builder.SetBasePath(Directory.GetCurrentDirectory());
            return builder.Build();
        }


        public static DocumentStore GetDocumentStore()
        {
            if (store != null)
            {
                return store;
            }

            var config = GetConfiguration();

            var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTIONSTRING_TESTS") ?? config.GetValue<string>("connectionStrings:postgres") ?? throw new ArgumentNullException("No postgres connection string!");

            store = DocumentStore.For(options =>
            {
                options.Connection(connectionString);
                options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
                options.UseDefaultSerialization(nonPublicMembersStorage: NonPublicMembersStorage.All);
            });

            return store;
        }




    }
}
