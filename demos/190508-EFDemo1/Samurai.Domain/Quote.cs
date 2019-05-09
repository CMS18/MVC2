namespace SamuraiApp.Domain
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }

        // Många till en (N-1)
        public Samurai Samurai { get; set; }
        public int SamuraiId { get; set; }
    }
}
