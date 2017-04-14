using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakesMocks
{
    public interface INotificationProcessor
    {
        void Notify(String message, NotificationTypes notificationType);
    }
}
