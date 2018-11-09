using System;
using System.Collections.Generic;
using System.Text;

namespace FindMe
{
    public class Response
    {
        public bool IsSuccessStatusCode { get; set; }
        public string Content { get; set; }
        public string ReasonPhrase { get; set; }
        public int StatusCode { get; set; }
    }
}
