namespace PatientMs.Controller.Dto
{
    public struct ErrorDto
    {
        public ErrorDto(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
