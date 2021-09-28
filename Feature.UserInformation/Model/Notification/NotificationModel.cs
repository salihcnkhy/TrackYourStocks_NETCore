using System;

namespace Feature.UserInformation.Model
{
    public class NotificationModel
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public bool HasShown { get; set; }
        public DateTime Date { get; set; }
        public string IconName { get; set; }
    }
}
