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

    public delegate void CheckInStatusHandler(object sender, GeopAppApiEventArgs e);

    public class CheckInRequest
    {
        private static GeoAppStorage settings = new GeoAppStorage();
        private string mainServer = settings.mainServer;
        private string deviceId = settings.DeviceId;

        public event CheckInStatusHandler CheckInStatus;


        /// <summary>
        /// Initializes a new instance of the <see cref="CheckIn"/> class.
        /// </summary>
        public CheckInRequest() { }

        /// <summary>
        /// Checkins the specified latitude.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="time">The time.</param>
        /// <param name="devId">The dev id.</param>
        /// <returns>
        /// <c>true</c>: When request suceeded
        /// <c>false</c>: When request failed
        /// </returns>
        public void CheckIn(string latitude, string longitude, string time)
        {
            string responseResult = null;
            string uriPath = settings.mainServer + "/api/device/check-in";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("device_id", this.deviceId);
            parameters.Add("latitude", latitude);
            parameters.Add("longitude", longitude);
            parameters.Add("time", time);

            PostClient proxy = new PostClient(parameters);
            proxy.DownloadStringCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    //Process the result
                    responseResult = e.Result;
                    if (responseResult.Contains("OK"))
                    {
                        CheckInStatus(this, new GeopAppApiEventArgs("Sucessfully Checked In"));
                    }
                    else
                    {
                        CheckInStatus(this, new GeopAppApiEventArgs(new Exception("Failed to Check-in")));
                    }
                }
                else
                {
                    CheckInStatus(this, new GeopAppApiEventArgs(new Exception("Failed to Check-in")));
                }
            };
            proxy.DownloadStringAsync(new Uri(uriPath, UriKind.Absolute));
        }

    }
}
