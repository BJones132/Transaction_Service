using Transaction_Service.Data.Models;

namespace Transaction_Service.Data
{
    public class TransactionDTO
    {
        public required int sourceAccountId { get; set; }
        public required int destinationAccountId { get; set; }
        public required string reference { get; set; }
        public required string designator { get; set; }
        public required decimal amount { get; set; }
        public required decimal destinationBalance { get; set; }

        public TransactionDTO() { }
        public TransactionDTO(Transaction transaction) =>
            (sourceAccountId, destinationAccountId, reference, designator, amount, destinationBalance) =
            (transaction.sourceAccountId, transaction.destinationAccountId, transaction.reference, transaction.designator, transaction.amount, transaction.destinationBalance);
    }
}
