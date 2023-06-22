namespace PatientMs.Controller.Dto
{
    public struct CommunicationDto
    {
        public long patientId { get; set; }
        public long refId { get; set; }
        public DateTime scheduledDate { get; set; }
        public string content { get; set; }
        public string description { get; set; }
        public string medium { get; set; }
        public CommunicationReminderDto reminder { get; set; }
    }

    public struct CommunicationReminderDto
    {
        public int amount { get; set; }
        public DateTime deadline { get; set; }
    }
}
