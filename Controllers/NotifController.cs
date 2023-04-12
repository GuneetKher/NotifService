using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NotifService.Models;
using NotifService.Services;

namespace NotificationsApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly DbService _notificationsService;

        public NotificationsController(DbService notificationsService)
        {
            _notificationsService = notificationsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(Notif notification)
        {
            var result = await _notificationsService.CreateNotification(notification);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetNotifications(string userId)
        {
            var notifications = await _notificationsService.GetNotifications(userId);

            if (notifications == null)
            {
                return NotFound();
            }

            return Ok(notifications);
        }

        [HttpGet("{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsSeen(string notificationId)
        {
            var result = await _notificationsService.MarkNotificationAsSeen(notificationId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
