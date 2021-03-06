using FileUpload.Helper.Model;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FileUpload.Helper.Model.NotificationModel;

namespace FileUpload.Helper.Notification
{
    public interface INotification
    {
        IReadOnlyCollection<NotificationModel> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(string key, string message, ENotificationType notificationType);
        void AddNotification(string key, string message);
        void AddNotifications(IReadOnlyCollection<NotificationModel> notifications);
        void AddNotifications(IList<NotificationModel> notifications);
        void AddNotifications(ICollection<NotificationModel> notifications);
        void AddNotifications(ValidationResult validationResult);
    }
}
