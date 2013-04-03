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
using System.Collections.Generic;
using GeoAuthApp;

namespace GeoAuthApi
{

    public delegate void RegisterStatusHandler(object sender, GeopAppApiEventArgs e);

    /// <summary>
    /// Implements the /api/device/register call
    /// </summary>
    /// <author>Andrew From (fromx010)</author>
    public class RegisterRequest
    {
        private static GeoAppStorage settings = new GeoAppStorage();
        private string mainServer = settings.mainServer;
        private string deviceKey = settings.DeviceKey;

        public event RegisterStatusHandler RegisterStatus;


        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterRequest"/> class.
        /// </summary>
        public RegisterRequest() { }

        /// <summary>
        /// Registers the specified device.
        /// </summary>
        /// <param name="deviceKey">The device key.</param>
        /// <param name="deviceName">Name of the device.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void RegisterDevice(string deviceName, string username, string password)
        {
            string uriPath = settings.mainServer + "/api/device/register";
            string responseResult = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (this.deviceKey != null)
            {
                parameters.Add("device_key", this.deviceKey);
            }
            else
            {
                return;
            }

            parameters.Add("device_name", deviceName);
            parameters.Add("username", username);
            parameters.Add("password", password);

            PostClient proxy = new PostClient(parameters);
            proxy.DownloadStringCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    //save the response/result
                    responseResult = e.Result;
                    if (responseResult != null)
                    {
                        settings.DeviceId = responseResult;
                        RegisterStatus(this, new GeopAppApiEventArgs("Successfully Registered"));
                    }
                    else
                    {
                        RegisterStatus(this, new GeopAppApiEventArgs(new Exception("Failed to register")));
                    }

                }
                else
                {
                    RegisterStatus(this, new GeopAppApiEventArgs(new Exception("Failed to register")));
                }
            };
            proxy.DownloadStringAsync(new Uri(uriPath, UriKind.Absolute));
        }
    }
}
