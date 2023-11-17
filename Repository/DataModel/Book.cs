namespace LibraryApp.Repository.DataModel
{
    public class Book
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; }
    }
}
