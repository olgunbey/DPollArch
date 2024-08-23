using DPoll.Domain.Common;
using System.Text.Json;

namespace DPoll.Domain.Entities
{
    public class Slide:BaseEntity
    {
        public Guid PresentationId { get; set; }
        public int Index { get; set; }
        public string Type { get; set; }
        public JsonDocument Content { get; set; }
        public bool IsVisible{ get; set; }

    }
}
