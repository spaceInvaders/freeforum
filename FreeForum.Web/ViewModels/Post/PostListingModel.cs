namespace FreeForum.ViewModels.Post
{
    public class PostListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string DateCreated { get; set; }
        public string Content { get; set; }
        public int RepliesCount { get; set; }
    }
}
