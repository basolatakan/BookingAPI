﻿using BookingAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.Response
{
    public class Response : IResponse   //Veri dönmeyen senaryolar için
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public Response(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static Response Success(string message = "") //bir cevap gelmezse null atacak
        {
            return new Response(true, message);
        }

        public static Response Error(string message = "") 
        {
            return new Response(false, message);
        }
    }
}
