using System;
using System.Net;
using System.Reflection;

namespace PrintPDV.Utility
{
    public class WebServiceUtility
    {
        public class RequestResponse
        {
            public bool Success { get; set; }

            public string Message { get; set; }
        }

        public static RequestResponse Post(string url, string json)
        {
            try
            {
                using (var client = new WebClient())
                {
                    return client.UploadString(new Uri(url), json).FromJSON<RequestResponse>();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public delegate void PostAsyncOnCompleteEventHandler(RequestResponse response);
        public static event PostAsyncOnCompleteEventHandler PostAsyncOnComplete;

        public static void PostAsync(string url, string json)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.UploadStringAsync(new Uri(url), json);
                    client.UploadStringCompleted += (s, args) =>
                    {
                        if (args.Error != null)
                            throw args.Error;

                        if (args.Cancelled)
                            throw new Exception("Request has been canceled!"); //TODO: put on resource file

                        PostAsyncOnComplete(args.Result.FromJSON<RequestResponse>());
                    };
                }
            }
            catch (Exception ex)
            {
                LogUtility.Log(LogUtility.LogType.SystemError, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }
    }
}
