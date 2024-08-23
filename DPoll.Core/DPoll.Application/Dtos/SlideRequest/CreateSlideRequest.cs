using System.Text.Json;

namespace DPoll.Application.Dtos.SlideRequest;
public class CreateSlideRequest
{
    public string Type { get; set; }
    public JsonDocument Content { get; set; }
}
