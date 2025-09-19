using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.Response
{
    public class NoContentResponse : Response
    {
        public NoContentResponse(string message = "") : base(true, message ?? "No Content")
        {

        }

        public static NoContentResponse Success(string message = "") 
        {
            return new NoContentResponse(message);
        }
    }
}
