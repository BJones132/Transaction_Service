namespace Transaction_Service.Data.Models
{
    public class Transaction
    {
        public int id { get; set; }
        public required int sourceAccountId { get; set; }
        public required int destinationAccountId { get; set; }
        public required string reference { get; set; }
        public required string designator { get; set; }
        public required decimal amount { get; set; }
        public required decimal destinationBalance { get; set; }
    }
}
