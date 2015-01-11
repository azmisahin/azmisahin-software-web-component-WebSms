namespace @as.Componet.Sms
{
    /// <summary>
    /// Country Default
    /// </summary>
    public enum Country { Turkiye};

    /// <summary>
    /// Error
    /// </summary>
    public enum Results { Error, Ok}

    /// <summary>
    /// Sms Manager Interface
    /// </summary>
    public interface ISmsManager
    {
        /// <summary>
        /// Default Turkey
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        ISmsManager SetCountry(Country country);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number">int.length 10</param>
        /// <returns></returns>
        ISmsManager SetNumber(string number);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">string.length max 140</param>
        /// <returns></returns>
        ISmsManager SetMessage(string message);

        /// <summary>
        /// Send Fonction
        /// </summary>
        /// <returns></returns>
        Results Send();
    }
}
