namespace TicketAssignerAPI.Model
{
    public class Ticket
    {
        public string TicketId { get; set; }
        public string Category { get; set; }
        public float TotalEstimate { get; set; }
        public float LoggedEstimate { get; set; }
        public string Priority { get; set; }
        public string AssignedTo { get; set; }
        public float TicketCount { get; set; }
        public bool IsAssigned { get; set; }
    }
}
