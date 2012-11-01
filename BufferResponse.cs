using Hammock;
using System;
using System.Net;

namespace BufferAPI
{
    public class BufferResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public RestRequest Request { get; set; }
        public RestResponse Response { get; set; }
        public string Contents { get; set; }
        public Exception InternalException { get; set; }
        public BufferService Sender { get; set; }
    }
}
