﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class MessageModel
    {
        public User Sender { get; set; }
        public string Message { get; set; }
    }
}
