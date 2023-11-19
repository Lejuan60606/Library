namespace Repository.DataModel
{
    public class BorrowTransaction
    {
        public string Id { get; set; }
        public string BookID { get; set; }
        public string MemberID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
