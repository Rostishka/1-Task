using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakesMocks
{
     public class CalculationProcessor : ICalculation
    {
        private readonly INotificationProcessor _notificationProcessor;
        private readonly IResultStorage _storage;

        public CalculationProcessor(IResultStorage storage, INotificationProcessor notificationProcessor)
        {
            _notificationProcessor = notificationProcessor;
            _storage = storage;
        }

        public void Calculate(object a, object b)
        {
            String resust = null;
            try
            {
                resust = String.Format("{{ {0}; {1}}}", a.GetType().Name, b.GetType().Name);
                _storage.Save(resust);
                _notificationProcessor.Notify(resust, NotificationTypes.Success);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _notificationProcessor.Notify(resust, NotificationTypes.Error);
            }
        }
    }
}
