using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotifService.Models;
using System.Text;
using MongoDB.Bson;

namespace NotifService.Services
{
    public class DbService
    {
        private readonly IMongoCollection<Notif> _notifs;
        public DbService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _notifs = database.GetCollection<Notif>(settings.UsersCollectionName);
        }

        public async Task<Notif> CreateNotification(Notif notification)
        {
            try
            {
                await _notifs.InsertOneAsync(notification);
                return notification;
            }
            catch (MongoException ex)
            {
                // handle exception
                return null;
            }
        }

        public async Task<IEnumerable<Notif>> GetNotifications(string userId)
        {
            try
            {
                var filter = Builders<Notif>.Filter.Eq(n => n.UserID, userId) & Builders<Notif>.Filter.Eq(n => n.Seen, false);
                return await _notifs.Find(filter).ToListAsync();
            }
            catch (MongoException ex)
            {
                // handle exception
                return null;
            }
        }

        public async Task<Notif> MarkNotificationAsSeen(string notificationId)
        {
            try
            {
                var filter = Builders<Notif>.Filter.Eq(n => n.Id, notificationId);
                var update = Builders<Notif>.Update.Set(n => n.Seen, true);
                return await _notifs.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<Notif> { ReturnDocument = ReturnDocument.After });
            }
            catch (MongoException ex)
            {
                // handle exception
                return null;
            }
        }

    }
}