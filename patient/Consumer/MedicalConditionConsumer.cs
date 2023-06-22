using PatientMs.Consumer.Dto;
using PatientMs.Service;
using PatientMs.Utils;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Transactions;

namespace PatientMs.Consumer
{
    public class MedicalConditionConsumer : MessageConsumer
    {

        private IMedicalConditionService service;

        public MedicalConditionConsumer(ConnectionFactory connectionFactory, IMedicalConditionService medicalConditionService) : base(connectionFactory)
        {
            service = medicalConditionService;
        }

        public override void Initialize()
        {


            registerQueueListener("add-medical-condition", (object? _, BasicDeliverEventArgs eventargs) =>
            {
                Console.WriteLine("add medical condition received");
                try
                {
                    
                    var dto = JsonSerializer.Deserialize<AddMedicalConditionDto>(Encoding.UTF8.GetString(eventargs.Body.ToArray()));
                    service.AddMedicalCondition(dto.patientId, dto.toMedicalCondition());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            );

            registerQueueListener("remove-medical-condition", (object? _, BasicDeliverEventArgs eventargs) =>
            {
                Console.WriteLine("Remove medical condition received");
                try
                {
                    var dto = JsonSerializer.Deserialize<AddMedicalConditionDto>(Encoding.UTF8.GetString(eventargs.Body.ToArray()));
                    service.RemoveMedicalConditon(dto.patientId, dto.toMedicalCondition());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            );


        }
    }
}
