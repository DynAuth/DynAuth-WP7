using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GeoAuthApp;
using System.Collections.Generic;

namespace GeoAuthApi
{

    public delegate void DeviceKeyStatusHandler(object sender, GeopAppApiEventArgs e);

    public class RequestDeviceKey
    {
        private static GeoAppStorage settings = new GeoAppStorage();
        private string mainServer = settings.mainServer;
        private string apiKey = settings.apiKey;

        public event DeviceKeyStatusHandler DeviceKeyStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestDeviceKey"/> class.
        /// </summary>
        public RequestDeviceKey() { }

        /// <summary>
        /// Request a new device key registered to a specific API key.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <returns>
        /// <c>true</c>: When request suceeded
        /// <c>false</c>: When request failed
        /// </returns>
        public void RequestDevKey()
        {
            string uriPath = settings.mainServer + "/api/service/request-device-key";
            string responseResult = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("api_key", this.apiKey);

            PostClient proxy = new PostClient(parameters);
            proxy.DownloadStringCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    //save the response/result
                    responseResult = e.Result;
                    if (responseResult != null)
                    {
                        //store the device key
                        settings.deviceKey = responseResult;
                        DeviceKeyStatus(this, new GeopAppApiEventArgs("Device Key reccieved"));
                    }
                    else
                    {
                        DeviceKeyStatus(this, new GeopAppApiEventArgs(new Exception("Error retrieving device key")));
                    }
                }
                else
                {
                    DeviceKeyStatus(this, new GeopAppApiEventArgs(new Exception("Error retrieving device key")));
                }
            };
            proxy.DownloadStringAsync(new Uri(uriPath, UriKind.Absolute));
        }
    }
}
