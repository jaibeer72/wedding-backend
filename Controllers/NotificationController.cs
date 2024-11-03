using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using wedding_backend.Hubs;

namespace wedding_backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationController(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("notify")]
    public async Task<IActionResult> Notify([FromBody] NotificationMessage message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
        return Ok("Notification sent");
    }
}

public class NotificationMessage
{
    public string Title { get; set; }
    public string Message { get; set; }
}