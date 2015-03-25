using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace QQWpfApplication1.action
{
    public class QQHttpResponse
    {
        private System.Net.Http.HttpResponseMessage response;

        public QQHttpResponse(System.Net.Http.HttpResponseMessage response)
        {
            // TODO: Complete member initialization
            this.response = response;
        }

        public String getResponseMessage()
        {
            return Encoding.UTF8.GetString(respData);
        }



        public byte[] respData { get; set; }

        internal HttpStatusCode getStatusCode()
        {
            return response.StatusCode;
        }

        internal string getResponseString()
        {
            return getResponseMessage();
        }

        internal byte[] getResponseData()
        {
            return respData;
        }
        public String ToString()
        {
            return getResponseMessage();
        }
    }
}
