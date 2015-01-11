using System;
using System.Configuration;//System.Configuration.dll
namespace @as.Componet.Sms
{
    /// <summary>
    /// Sms Gönderim Merkezi
    /// </summary>
    public class SmsManager : ISmsManager
    {
        #region Field        
        private Uri _uri;
        private int _id;
        private string _user;
        private string _password;
        public string _title;

        private Results _result;
        private Country _country;
        private string _message;
        private string _number;
        #endregion

        #region Ctor
        /// <summary>
        /// Sms Manager
        /// </summary>
        public SmsManager()
        {
            _uri        = new Uri(getAppKey("Componet.Sms.Uri"));
            _id         = Convert.ToInt32(getAppKey("Componet.Sms.Id"));
            _user       = getAppKey("Componet.Sms.User");
            _password   = getAppKey("Componet.Sms.Password");
            _title      = getAppKey("Componet.Sms.Title");            
            _country    = Country.Turkiye;            
            _message    = DateTime.Now.ToShortDateString();
        }
        #endregion
        
        #region Configuration
        /// <summary>
        /// Application Key
        /// </summary>
        /// <param name="key">appSettings Key</param>
        /// <returns>value</returns>
        private string getAppKey(string key)
        {
            var value = ConfigurationManager.AppSettings[key].ToString();
            return value;
        }
        #endregion
        
        #region Function
        /// <summary>
        /// Send
        /// </summary>
        /// <returns></returns>
        public Results Send()
        {
            return CallSend();
        }
        /// <summary>
        /// Set Country
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public ISmsManager SetCountry(Country country)
        {
            _country = country;
            return this;
        }
        /// <summary>
        /// Set Message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ISmsManager SetMessage(string message)
        {
            _message = message;
            return this;
        }

        /// <summary>
        /// Set Number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public ISmsManager SetNumber(string number)
        {
            _number = number;
            return this;
        }
        #endregion
        
        #region Internal Function
        /// <summary>
        /// Call Send
        /// </summary>
        /// <returns></returns>
        private Results CallSend()
        {
            var smsSetting = new SmsSetting(id:_id,user:_user, password:_password, title:_title);
            string queryFormat = "&apiNo={0}&user={1}&pass={2}&numaralar={3}&mesaj={4}&baslik={5}";
            string query = string.Format(queryFormat, smsSetting.id, smsSetting.user, smsSetting.password, _number, _message, _title);
            _result = Results.Ok;
            string[] ret = Post(query);
            if (string.IsNullOrEmpty(ret[0])) { _result = Results.Error; } else { _result = Results.Ok; }
            return _result;
        }
        /// <summary>
        /// Post Method
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        private string[] Post(string q)
        {
            string[] result = new string[2];
            try
            {
                string strNewValue;
                string strResponse;
                System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(_uri);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                strNewValue = req + q;
                req.ContentLength = strNewValue.Length;
                System.IO.StreamWriter stOut = new System.IO.StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                stOut.Write(strNewValue);
                stOut.Close();
                System.IO.StreamReader stIn = new System.IO.StreamReader(req.GetResponse().GetResponseStream());
                strResponse = stIn.ReadToEnd();
                stIn.Close();

                string r = "";
                if (strResponse.ToLower().IndexOf("hata") != -1) { r = "error"; }
                else { r = "ok"; result[1] = strResponse.ToLower().Substring(strResponse.IndexOf(":") + 1); }

                result[0] = r;
                return result;
            }
            catch (Exception)
            { result[0] = "error"; return result; }
        }
        #endregion

        #region SmsSetting
        private class SmsSetting
        {
            /// <summary>
            /// 1 Sending Operation
            /// 2 ?
            /// 3 ?
            /// 4 ?
            /// 5 ? 
            /// </summary>
            public int id { get; set; }

            /// <summary>
            /// Api User
            /// </summary>
            public string  user { get; set; }

            /// <summary>
            /// Api Password
            /// </summary>
            public string password { get; set; }

            /// <summary>
            /// Api Title
            /// </summary>
            public string title { get; set; }

            /// <summary>
            /// Sms Setting Configuration
            /// </summary>
            /// <param name="id"></param>
            /// <param name="user"></param>
            /// <param name="password"></param>
            /// <param name="title"></param>
            public SmsSetting(int id, string user, string password, string title)
            {
                this.id         = id;
                this.user       = user;
                this.password   = password;
                this.title      = title;
            }
        }
        #endregion
    }
}
