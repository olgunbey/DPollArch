using DPoll.Domain.Common;

namespace DPoll.Domain.Entities
{
    public class Presentation(Guid userId, string title):BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid UserId { get; set; } = userId;
        public string Title { get; set; } = title;
    }
}
