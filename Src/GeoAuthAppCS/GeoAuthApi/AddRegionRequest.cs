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

    public delegate void AddRegionStatusHandler(object sender, GeopAppApiEventArgs e);

    public class AddRegionRequest
    {
        private static GeoAppStorage settings = new GeoAppStorage();
        private string mainServer = settings.mainServer;
        private string deviceId = settings.DeviceId;

        public event AddRegionStatusHandler AddRegionStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRegionRequest"/> class.
        /// </summary>
        public AddRegionRequest() { }

        /// <summary>
        /// Adds a location region with an associated name.
        /// </summary>
        /// <param name="deviceId">A 32-character device API key assigned to the device when it was registered.</param>
        /// <param name="latitude">The latitude of the location region.</param>
        /// <param name="longitude">The longitude of the location region.</param>
        /// <param name="name">The given name to this region.</param>
        /// <param name="time">(OPTIONAL, NULLABLE)The time the update took place.</param>
        /// <param name="radius">(OPTIONAL, NULLABLE)The radius of the circular area which should be considered part of this region.</param>
        /// <returns>
        /// <c>true</c>: When request suceeded
        /// <c>false</c>: When request failed
        /// </returns>
        public void AddRegion(string latitude, string longitude, string name, string time, string radius)
        {
            string uriPath = settings.mainServer + "/api/device/add-region";
            string responseResult = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("device_id", this.deviceId);
            parameters.Add("latitude", latitude);
            parameters.Add("longitude", longitude);
            parameters.Add("name", name);
            //Check for optinal values (NULLABLE)
            if (time != null)
            {
                parameters.Add("time", time);
            }
            if (radius != null)
            {
                parameters.Add("radius", radius);
            }

            PostClient proxy = new PostClient(parameters);
            proxy.DownloadStringCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    //Process the result
                    responseResult = e.Result;
                    if (responseResult.Contains("OK"))
                    {
                        AddRegionStatus(this, new GeopAppApiEventArgs("Added Region"));
                    }
                    else
                    {
                        AddRegionStatus(this, new GeopAppApiEventArgs("Add Region FAILED"));
                    }
                }
                else
                {
                    AddRegionStatus(this, new GeopAppApiEventArgs("Add Region FAILED"));
                }
            };
            proxy.DownloadStringAsync(new Uri(uriPath, UriKind.Absolute));
        }
    }
}
