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
        private static ManualResetEvent apiCallEvent = new ManualResetEvent(false);
        private string postDataRequest;
        private string apiResponseMsg;

        public GeoAuthAPI()
        {
        }

        HttpWebRequest httpWReq =
    (HttpWebRequest)WebRequest.Create(@"http:\\domain.com\page.asp");

        /// <summary>
        /// Request a new device key registered to a specific API key.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <returns>
        /// <c>true</c>: When request suceeded
        /// <c>false</c>: When request failed
        /// </returns>
        public bool RequestDevKey(string apiKey)
        {
            return false;
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
        public bool Register(string deviceKey, string deviceName, string username, string password)
        {
            return false;
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
        public bool Checkin(string deviceId, string latitude, string longitude, string time)
        {
            List<PostData> postDataList = new List<PostData>();

            PostData _deviceId = new PostData("device_id", deviceId);
            postDataList.Add(_deviceId);

            PostData _latitude = new PostData("latitude", latitude);
            postDataList.Add(_latitude);

            PostData _longitude = new PostData("longitude", longitude);
            postDataList.Add(_longitude);
            PostData _time      = new PostData("time", time);
            postDataList.Add(_time);

            //Generate and store the Request String for POST
            this.postDataRequest = GeneratePostString(postDataList);
            //Now do the API Call
            ApiRequest();

            if (this.apiResponseMsg.Contains("OK"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a location region with an associated name.
        /// </summary>
        /// <param name="deviceId">A 32-character device API key assigned to the device when it was registered.</param>
        /// <param name="latitude">The latitude of the location region.</param>
        /// <param name="longitude">The longitude of the location region.</param>
        /// <param name="name">The given name to this region.</param>
        /// <param name="time">(OPTIONAL)The time the update took place.</param>
        /// <param name="radius">(OPTIONAL)The radius of the circular area which should be considered part of this region.</param>
        /// <returns>
        /// <c>true</c>: When request suceeded
        /// <c>false</c>: When request failed
        /// </returns>
        public bool AddRegion(string deviceId, string latitude, string longitude, string name, string time, string radius)
        {
            List<PostData> postDataList = new List<PostData>();

            PostData _deviceId  = new PostData("device_id", deviceId);
            postDataList.Add(_deviceId);

            PostData _latitude  = new PostData("latitude", latitude);
            postDataList.Add(_latitude);

            PostData _longitude = new PostData("longitude", longitude);
            postDataList.Add(_longitude);

            PostData _name      = new PostData("name", name);
            postDataList.Add(_name);

            //Optional values check for null
            if (time != null)
            {
                PostData _time = new PostData("time", time);
                postDataList.Add(_time);
            }
            if (radius != null)
            {
                PostData _radius = new PostData("radius", radius);
                postDataList.Add(_radius);
            }

            //Generate and store the Request String for POST
            this.postDataRequest = GeneratePostString(postDataList);
            //Now do the API Call
            ApiRequest();

            //check the result
            if(this.apiResponseMsg.Contains("OK"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GeneratePostString(List<PostData> postDataList)
        {
            string postString = "";
            for (int i = 0; i < postDataList.Count; i++)
            {
                if (i == 0)
                {
                    postString = postDataList[i].toHtmlCodingAndPair();
                }
                else
                {
                    postString = postString + "&" + postDataList[i].toHtmlCodingAndPair();
                }
            }
            return postString;
        }

        /// <summary>
        /// APIs the request.
        /// </summary>
        /// <returns></returns>
        private string ApiRequest()
        {
            string serverUri = null;

            HttpWebRequest apiRequest = (HttpWebRequest)WebRequest.Create(serverUri);

            apiRequest.Method = "POST";
            apiRequest.ContentType = "application/x-www-form-urlencoded";

            apiRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), apiRequest);

            // Wait until our POST request has been fully processed
            apiCallEvent.WaitOne();

            //Return the Servers Response
            return this.apiResponseMsg;
        }

        /// <summary>
        /// Gets the request stream async callback.
        /// </summary>
        /// <param name="asynchronousResult">The asynchronous result.</param>
        /// <remarks>
        /// Set the this.postDataRequest before calling. <br />
        /// Will create a async response event.
        /// </remarks>
        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            // End the operation
            Stream postStream = request.EndGetRequestStream(asynchronousResult);
            // Get the string to post
            string postData = this.postDataRequest;

            // Convert the string into a byte array. 
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Write to the request stream.
            postStream.Write(byteArray, 0, postData.Length);
            postStream.Close();

            // Start the asynchronous operation to get the response
            request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
        }

        /// <summary>
        /// Gets the response callback.
        /// </summary>
        /// <param name="asynchronousResult">The asynchronous result.</param>
        /// <remarks>
        /// Notifys the waiting threads that the Web Request has finished and saves
        /// the server response in this.apiResponseMsg.
        /// </remarks>
        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            // End the operation
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);

            //Set the response message
            this.apiResponseMsg = streamRead.ReadToEnd();

            // Close the stream object
            streamResponse.Close();
            streamRead.Close();

            // Release the HttpWebResponse
            response.Close();
            apiCallEvent.Set();
        }
    }

    /// <summary>
    /// Helper class that stores the post data pairs and
    /// useful methods.
    /// </summary>
    public class PostData
    {
        public string parameter;
        public string value;

        public PostData(string parameter, string value)
        {
            this.parameter = parameter;
            this.value = value;
        }

        /// <summary>
        /// Converts parameter and value to html encoding and pairs them.
        /// </summary>
        /// <returns></returns>
        public string toHtmlCodingAndPair()
        {
            if ((this.parameter != null) && (this.value != null))
            {
                return System.Net.HttpUtility.HtmlEncode(this.parameter) + "=" + System.Net.HttpUtility.HtmlEncode(this.value);
            }
            else
            {
                return null;
            }
        }
    }
}
