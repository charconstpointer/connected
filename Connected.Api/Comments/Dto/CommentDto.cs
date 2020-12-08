using System;

namespace Connected.Api.Comments.Dto
{
    
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string Author { get; set; }
    }
}