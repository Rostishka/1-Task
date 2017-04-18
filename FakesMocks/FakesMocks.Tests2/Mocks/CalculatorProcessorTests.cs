using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;
using Moq;
namespace FakesMocks.Tests2.Mocks
{   [TestFixture]
    class CalculatorProcessorTests
    {
        [Test]
        public void Calculate_ResultStorageThrowsExeption_SendsErrorNotification()
        {
            //Arrange
            //Setup Stub
            var resultStorageStub = new Mock<IResultStorage>();//Proxy Object побудований над IResultStorage-ом
            var exeption = new IOException("Sorry something wrong!");
            resultStorageStub.Setup(x => x.Save(It.IsAny<String>())).Throws(exeption);

            //SetUp Mock
            var notificationProcessorStub = new Mock<INotificationProcessor>();

            //Setp Object under Test
            var calculationProcessor = new CalculationProcessor(resultStorageStub.Object, notificationProcessorStub.Object);

            //Act
            var a = new Object();
            var b = String.Empty;
            calculationProcessor.Calculate(a, b);

            //Assertgf
            notificationProcessorStub.Verify(x => x.Notify(exeption.Message, NotificationTypes.Error), Times.Once);
        }

        [Test]
        public void Calculate_SaveResultsToStorage_SendsSuccessNotification()
        {
            //Arrange
            //Setup Stub
            var resultStorageStub = new Mock<IResultStorage>();//Proxy Object побудований над IResultStorage-ом
            resultStorageStub.Setup(x => x.Save(It.IsAny<String>()));//метод Setup - для тогго щоб вказати що саме ми оцікуємо від даного обєкта,
            //тут ми очікуємо що буде викликано метод сейв і тут нас не цікавить що саме нам прийде на вхід люба стрічка
            
            //SetUp Mock
            var notificationProcessorStub = new Mock<INotificationProcessor>();
            //Setp Object under Test
            var calculationProcessor = new CalculationProcessor(resultStorageStub.Object, notificationProcessorStub.Object);



            //Act
            var a = new Object();
            var b = String.Empty;
            calculationProcessor.Calculate(a, b);

            //Assert
            notificationProcessorStub.Verify(x => x.Notify("{ Object; Object }", NotificationTypes.Success), Times.Once);
        }
    }
}
