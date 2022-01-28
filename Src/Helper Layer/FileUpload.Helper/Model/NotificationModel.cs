﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.Helper.Model
{
    public class NotificationModel
    {
        public Guid NotificationId { get; private set; }
        public string Key { get; private set; }
        public string Message { get; private set; }
        public ENotificationType NotificationType { get; set; }

        public NotificationModel(string key, string message, ENotificationType notificationType = ENotificationType.BusinessRules)
        {
            NotificationId = Guid.NewGuid();
            Key = key;
            Message = message;
            NotificationType = notificationType;
        }

        public enum ENotificationType : byte
        {
            Default = 0,
            InternalServerError = 1,
            BusinessRules = 2,
            NotFound = 3
        }
    }

}
