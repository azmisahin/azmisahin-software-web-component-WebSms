using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace @as.Componet.Sms.Test
{
    [TestClass]
    public class SmsMessageTest
    {
        [TestMethod]
        public void SendMethod()
        {
            SmsMessage
                .Sender
                .SetNumber("1234567980")
                .SetMessage(GetType().Assembly.ToString())
                .Send();
        }
    }
}
