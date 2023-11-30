using Microsoft.AspNetCore.Mvc;
using WebHook_Notifications.Models.DTOs;
using WebHook_Notifications.Services;

namespace WebHook_Notifications.Controllers
{
    [ApiController]
    [Route("WebHook/Notification")]
    public class NotificationController : ControllerBase
    {
        private readonly IRepositoryNotification repository;

        public NotificationController(IRepositoryNotification repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        public async Task<ActionResult> CreateNotification([FromBody] NotificationDTO notificationDTO)
        {
            // Si ya la notificación existe en la base de datos entonces no la guardamos y devolvemos error
            var existsId = await repository.ExistsId(notificationDTO.RequestId);
            if (existsId)
            {
                return BadRequest();
            }
            await repository.InsertData(notificationDTO);
            return Ok();

        }


    }
}
