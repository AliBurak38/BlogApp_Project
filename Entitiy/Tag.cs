namespace NewBlogApp.Entitiy
{

    public enum TagColors 
    { 
           primary,secondary,warning,danger,success,info
               
    }
    public class Tag
    {
        public int TagId { get; set; }

        public string? Text { get; set; }

        public string? Url { get; set; }

        public TagColors? Color { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
