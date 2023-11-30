using Microsoft.AspNetCore.Mvc;
using WebHook_Notifications.Models.DTOs;

namespace WebHook_Notifications.Controllers
{
    [ApiController]
    [Route("WebHook/Notification")]
    public class NotificationController : ControllerBase
    {
        
        [HttpPost]
        public async Task<ActionResult> CreateNotification([FromBody] NotificationDTO notificationDTO)
        {
            
            return Ok();
            
        }

        
    }
}
