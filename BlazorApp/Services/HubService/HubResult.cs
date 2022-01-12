using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class HubResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Value { get; set; }
    }
}
