using System.Text.Json;

namespace DPoll.Application.Dtos.SlideRequest;
public class UpdateSlideRequest
{
    public int Index { get; set; }
    public string Type { get; set; }
    public bool IsVisible { get; set; }
    public JsonDocument Content { get; set; }
}
