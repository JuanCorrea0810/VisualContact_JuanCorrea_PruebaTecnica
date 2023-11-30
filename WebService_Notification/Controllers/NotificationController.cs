using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using WebService_Notification.Models.DTOs;
using WebService_Notification.Services;

namespace WebService_Notification.Controllers
{
    [ApiController]
    [Route("api/Notification")]
    public class NotificationController : ControllerBase
    {
        private readonly IRepositoryNotification repository;
        private readonly HttpClient _httpClient;

        public NotificationController(IRepositoryNotification repository, IHttpClientFactory httpClientFactory)
        {
            // El httpClient que nos va a servir para enviar las notificaciones al webhook
            _httpClient = httpClientFactory.CreateClient("Notification");
            this.repository = repository;
        }
        [HttpPost]
        public async Task<ActionResult> CreateNotification([FromBody] NotificationDTO notificationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Como no se si el parámetro "suscription" al final del JSON es una clase o un entero u otro tipo de dato
            // ya que en el ejemplo lo dan como null, entonces por defecto lo voy a tomar como un string
            if (notificationDTO.Subscription == null)
            {
                notificationDTO.Subscription = "";
            }
            //Si ya existe el Id entonces se manda un error, así se evita registros duplicados
            var idAlreadyExists = await repository.IdAlreadyExists(notificationDTO.RequestId);
            if (idAlreadyExists)
            {
                return BadRequest();
            }

            // Si response == 1, operación exitosa
            // response == 0, operación fallida
            var response = await repository.InserData(notificationDTO);
            if (response == 0)
            {
                return BadRequest();
            }

            //Mandar notificación al WebHook
            // En este caso estamos suscribiendo manualmente nuestro webhook a esta API
            // Pero si queremos suscribir varias URLs entonces se podría optar por otro método como utilizar RabbitMQ
            using var respuesta = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress, notificationDTO);

            return CreatedAtRoute("GetById", new { RequestId = notificationDTO.RequestId }, notificationDTO);
        }

        [HttpGet("{RequestId:int}", Name = "GetById")]
        public async Task<ActionResult<NotificationDTO>> GetById([FromRoute] int RequestId)
        {
            var response = await repository.GetNotification(RequestId);
            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
