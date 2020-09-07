namespace FreeForum.ViewModels.Reply
{
    public class ReplyEditModel
    {
        public int ReplyId { get; set; }
        public string AuthorId { get; set; }
        public string ContentToEdit { get; set; }

        public string PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }

        public string EditedContent { get; set; }
    }
}
