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
using System.IO;
using System.Threading;
using System.Text;

namespace GeoAuthApp
{
    /// <summary>
    /// Provides methods to invoke API Calls
    /// </summary>
    /// <author>Andrew From (fromx010)</author>
    public class GeoAuthAPI
    {
        private static GeoAppStorage settings = new GeoAppStorage();
        private string mainServer = settings.mainServer;
        private string apiKey = settings.apiKey;
        private string deviceId = settings.DeviceId;
        private string deviceKey = settings.deviceKey;


        public GeoAuthAPI()
        {
        }

        /// <summary>
        /// Request a new device key registered to a specific API key.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <returns>
        /// <c>true</c>: When request suceeded
        /// <c>false</c>: When request failed
        /// </returns>
        public ApiResponse RequestDeviceKey()
        {
            ApiResponse apiResponse = new ApiResponse();
            string uriPath = settings.mainServer + "/service/request-device-key";
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
                }
            };
            proxy.DownloadStringAsync(new Uri(uriPath, UriKind.Absolute));

            // Check if it was successful
            if ((responseResult != null) && (responseResult.Contains("OK")))
            {
                //TODO store the devicekey
                apiResponse.Success = true;
                apiResponse.HttpCode = HttpStatusCode.OK;
                apiResponse.Message = "Recieved device Id";
                return apiResponse;
            }
            else
            {
                apiResponse.Success = false;
                apiResponse.HttpCode = HttpStatusCode.Ambiguous;
                apiResponse.Message = responseResult;
                return apiResponse;
            }
        }

        /// <summary>
        /// Registers the specified device.
        /// </summary>
        /// <param name="deviceKey">The device key.</param>
        /// <param name="deviceName">Name of the device.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///   <c>true</c>: When sucessfully registered
        /// <c>false</c>: when registration failed
        /// </returns>
        public ApiResponse Register(string deviceName, string username, string password)
        {
            ApiResponse apiResponse = new ApiResponse();
            string uriPath = settings.mainServer + "/device/register";
            string responseResult = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            
            if (this.deviceKey != null)
            {
                parameters.Add("device_key", this.deviceKey);
            }
            else
            {
                apiResponse.Success = false;
                apiResponse.HttpCode = HttpStatusCode.Ambiguous;
                apiResponse.Message = "Missing Required device key";
                return apiResponse;
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
                }
            };
            proxy.DownloadStringAsync(new Uri(uriPath, UriKind.Absolute));

            // Check if it was successful
            if ((responseResult != null) && (responseResult.Contains("OK")))
            {
                apiResponse.Success = true;
                apiResponse.HttpCode = HttpStatusCode.OK;
                apiResponse.Message = "Successfully Registered Device";
                return apiResponse;
            }
            else
            {
                apiResponse.Success = false;
                apiResponse.HttpCode = HttpStatusCode.Ambiguous;
                apiResponse.Message = responseResult;
                return apiResponse;
            }
        }

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
        public ApiResponse Checkin(string latitude, string longitude, string time)
        {
            ApiResponse apiResponse = new ApiResponse();
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
                }
            };
            proxy.DownloadStringAsync(new Uri(uriPath, UriKind.Absolute));

            // Check if it was successful
            if((responseResult != null) && (responseResult.Contains("OK")))
            {
                apiResponse.Success = true;
                apiResponse.HttpCode = HttpStatusCode.OK;
                apiResponse.Message = "Successfully checked-in";
                return apiResponse;
            }
            else
            {
                apiResponse.Success = false;
                apiResponse.HttpCode = HttpStatusCode.Ambiguous;
                apiResponse.Message = responseResult;
                return apiResponse;
            }
        }

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
        public ApiResponse AddRegion(string latitude, string longitude, string name, string time, string radius)
        {
            ApiResponse apiResponse = new ApiResponse();
            string uriPath = settings.mainServer + "/api/device/add-region";
            string responseResult = null;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("device_id", this.deviceId);
            parameters.Add("latitude", latitude);
            parameters.Add("longitude", longitude);
            parameters.Add("name", name);
            //Check for optinal values (NULLABLE)
            if(time != null)
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
                }
            };
            proxy.DownloadStringAsync(new Uri(uriPath, UriKind.Absolute));

            // Check if it was successful
            if ((responseResult != null) && (responseResult.Contains("OK")))
            {
                apiResponse.Success = true;
                apiResponse.HttpCode = HttpStatusCode.OK;
                apiResponse.Message = "Successfully created region: " + name;
                return apiResponse;
            }
            else
            {
                apiResponse.Success = false;
                apiResponse.HttpCode = HttpStatusCode.Ambiguous;
                apiResponse.Message = responseResult;
                return apiResponse;
            }
        }
    }

    /// <summary>
    /// Object to hold data about the ApiRequest response
    /// </summary>
    public class ApiResponse
    {
        bool success;
        HttpStatusCode httpCode;
        string message;

        #region accessors
        public bool Success
        {
            get
            {
                return success;
            }
            set
            {
                this.success = value;
            }
        }
        public HttpStatusCode HttpCode 
        { 
            get
            {
                return httpCode;
            }

            set
            {
                this.httpCode = value;
            }
        }
        public string Message 
        { 
            get
            {
                return message;
            }
            set
            {
                this.message = value;
            }
        }
        #endregion

        #region constructors
        public ApiResponse(bool success, HttpStatusCode code, string message)
        {
            this.success = success;
            this.httpCode = code;
            this.message = message;
        }

        public ApiResponse()
        {
        }
        #endregion
    }
}
