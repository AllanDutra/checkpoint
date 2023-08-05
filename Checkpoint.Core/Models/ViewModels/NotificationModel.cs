using System.Net;

namespace Checkpoint.Core.Models.ViewModels
{
    public class NotificationModel
    {
        public NotificationModel(string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public string Message { get; }
        public HttpStatusCode StatusCode { get; }
    }
}
