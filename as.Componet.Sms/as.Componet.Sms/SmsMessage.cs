namespace @as.Componet.Sms
{
    /// <summary>
    /// Sms Message
    /// </summary>
    public class SmsMessage
    {
        /// <summary>
        /// Instance
        /// </summary>
        private static SmsManager instance = null;

        /// <summary>
        /// Sender
        /// </summary>
        public static ISmsManager Sender
        {
            get { return instance = null != instance ? instance : new SmsManager(); }
        }
    }
}