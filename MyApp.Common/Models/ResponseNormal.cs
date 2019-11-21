using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Common.Models
{
    public class ResponseNormal
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
