using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;

namespace GeoAuthApp
{
    /// <summary>
    /// Provides common methods for sending data to and receiving data from an HTTP POST web request.
    /// </summary>
    /// <remarks>
    /// This class was modifed from http://www.codeproject.com/Articles/198651/An-HTTP-POST-client-for-Windows-Phone-7
    /// </remarks>
    public class PostClient
    {
        #region Members

        /// <summary>
        /// Post data query string.
        /// </summary>
        StringBuilder postData = new StringBuilder();

        #endregion

        #region Events

        /// <summary>
        /// Event handler for DownloadStringCompleted event.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Argument holding the data downloaded.</param>
        public delegate void DownloadStringCompletedHandler(object sender, DownloadStringCompletedEventArgs e);

        /// <summary>
        /// Occurs when an asynchronous resource-download operation is completed.
        /// </summary>
        public event DownloadStringCompletedHandler DownloadStringCompleted;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of PostClient with the specified parameters.
        /// </summary>
        /// <param name="data">POST parameters in string format. Valid string format is something like "id=120&amp;name=John".</param>
        public PostClient(string parameters)
        {
            postData.Append(parameters);
        }

        /// <summary>
        /// Creates a new instance of PostClient with the specified parameters.
        /// </summary>
        /// <param name="parameters">POST parameters as a list of string. Valid list elements are "id=120" and "name=John".</param>
        public PostClient(IList<string> parameters)
        {
            foreach (var element in parameters)
            {
                postData.Append(string.Format("{0}&", element));
            }
        }

        /// <summary>
        /// Creates a new instance of PostClient with the specified parameters.
        /// </summary>
        /// <param name="parameters">POST parameters as a dictionary with string keys and values. Valid elements could have keys "id" and "name" with values "120" and "John" respectively.</param>
        public PostClient(IDictionary<string, object> parameters)
        {
            
            foreach (KeyValuePair<string,object> pair in parameters)
            {
                postData.Append(string.Format("{0}={1}&", pair.Key, pair.Value));
            }

            //Remove the trailing &, kinda of hackish
            string postDataString = postData.ToString().TrimEnd('&');
            postData.Clear();
            postData.Append(postDataString);
            
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Downloads the resource at the specified Uri as a string.
        /// </summary>
        /// <param name="address">The location of the resource to be downloaded.</param>
        public void DownloadStringAsync(Uri address)
        {
            HttpWebRequest request;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(address);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.BeginGetRequestStream(new AsyncCallback(RequestReady), request);
            }
            catch
            {
                if (DownloadStringCompleted != null)
                {
                    System.Windows.Deployment.Current.Dispatcher.BeginInvoke(delegate()
                    {
                        DownloadStringCompleted(this, new DownloadStringCompletedEventArgs(new Exception("Error creating HTTP web request.")));
                    });
                }
            }
        }

        #endregion

        #region Protected methods

        void RequestReady(IAsyncResult asyncResult)
        {
            HttpWebRequest request = asyncResult.AsyncState as HttpWebRequest;

            using (Stream stream = request.EndGetRequestStream(asyncResult))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(postData.ToString());
                    writer.Flush();
                }
            }

            request.BeginGetResponse(new AsyncCallback(ResponseReady), request);
        }

        void ResponseReady(IAsyncResult asyncResult)
        {
            HttpWebRequest request = asyncResult.AsyncState as HttpWebRequest;
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.EndGetResponse(asyncResult);

                string result = string.Empty;
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        result = reader.ReadToEnd();
                    }
                }

                if (DownloadStringCompleted != null)
                {
                    System.Windows.Deployment.Current.Dispatcher.BeginInvoke(delegate()
                    {
                        DownloadStringCompleted(this, new DownloadStringCompletedEventArgs(result));
                    });
                }
            }
            catch
            {
                if (DownloadStringCompleted != null)
                {
                    System.Windows.Deployment.Current.Dispatcher.BeginInvoke(delegate()
                    {
                        DownloadStringCompleted(this, new DownloadStringCompletedEventArgs(new Exception("Error getting HTTP web response.")));
                    });
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Provides data for the DownloadStringCompleted event.
    /// </summary>
    /// <remarks>
    /// This class was modifed from http://www.codeproject.com/Articles/198651/An-HTTP-POST-client-for-Windows-Phone-7
    /// </remarks>
    public class DownloadStringCompletedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the data that is downloaded by a DownloadStringAsync method.
        /// </summary>
        public string Result { get; private set; }

        /// <summary>
        /// Gets a value that indicates which error occurred during an asynchronous operation.
        /// </summary>
        public Exception Error { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of DownloadStringCompletedEventArgs with the specified result data.
        /// </summary>
        /// <param name="result">The data that is downloaded by a DownloadStringAsync method.</param>
        public DownloadStringCompletedEventArgs(string result)
        {
            Result = result;
        }

        /// <summary>
        /// Creates a new instance of DownloadStringCompletedEventArgs with the specified exception.
        /// </summary>
        /// <param name="ex">The exception generated by the asynchronous operation.</param>
        public DownloadStringCompletedEventArgs(Exception ex)
        {
            Error = ex;
        }

        #endregion
    }
}
