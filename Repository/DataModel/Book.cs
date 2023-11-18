namespace Repository.DataModel
{
    public class Book
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public DateTime? PublicationYear { get; set; }
        public string? IsAvailable { get; set; }
    }
}