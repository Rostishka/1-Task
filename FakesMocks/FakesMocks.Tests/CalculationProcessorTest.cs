using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace FakesMocks.Tests
{
    class CalculationProcessorTest
    {
        [TestFixture]
        public sealed class CalclationProcessorTest
        {
            [Test]
            public void Calculate_ResultStorageThrowsExeption_SendsErrorNotification()
            {
                const String exeptionMessage = "segdgdfhdh";
                var resultStorage = new FakeResltStorage()
                {
                    Exeption = new IOException(exeptionMessage)
                };

                var notificationProcessor = new FackeNotificationProcessor();
                var calculationProcessor = new CalculationProcessor(resultStorage, notificationProcessor);

                //Act
                var a = new Object();
                var b = new Object();
                calculationProcessor.Calculate(a, b);

                //Assert
                Assert.AreEqual(notificationProcessor.NotificationType, NotificationTypes.Error);
                Assert.AreEqual(notificationProcessor.Message, exeptionMessage);
            }

            [Test]
            public void Calculate_SaveResultsToStorage_SendsSuccessNotification()
            {
                //Arrange
                var resultStorage = new FakeResltStorage();
                var notificationProcessor = new FackeNotificationProcessor();
                var calculationProcessor = new CalculationProcessor(resultStorage, notificationProcessor);

                //Act
                var a = new Object();
                var b = String.Empty;
                calculationProcessor.Calculate(a, b);

                //Assert
                Assert.AreEqual(notificationProcessor.NotificationType, NotificationTypes.Success);
                Assert.AreEqual(notificationProcessor.Message, "{ Object; String }");
            }

            private sealed class FackeNotificationProcessor : INotificationProcessor
            {
                public String Message { get; set; }

                public NotificationTypes NotificationType { get; set; }

                public void Notify(string message, NotificationTypes notificationType)
                {
                    Message = message;
                    NotificationType = notificationType;
                }
            }

            private sealed class FakeResltStorage : IResultStorage
            {
                public Object Result { get; set; }

                public Int64 Id { get; set; }

                public Exception Exeption { get; set; }


                public long Save(object data)
                {
                    Result = data;
                    if (null != Exeption)
                    {
                        throw Exeption;
                    }
                    Id = 1;
                    return Id;
                }
            }
        }
    }
}
