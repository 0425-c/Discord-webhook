using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web;

namespace Webhook_
{
    internal class Functions
    {
        private static byte[] Post(string url, NameValueCollection pairs)
        {
            byte[] result;
            using (WebClient webClient = new WebClient())
            {
                result = webClient.UploadValues(url, pairs);
            }
            return result;
        }

        public static void Promotion(string URLs, string Content, string SenderName)
        {

            Functions.SendWebhook(URLs, Content, SenderName);

        }

        private static void SendWebhook(string URL, string content, string username)
        {
            try
            {
                Functions.Post(URL, new NameValueCollection
                {
                    {
                        "username",
                        username
                    },
                    {
                        "content",
                        content
                    }
                });
            }
            catch
            { }
        }
        public Functions()
        {
        }
    }
}

