using System;
using System.Web;

namespace Gamma.Domain.Entities.Response
{
     public class CookieResponse
     {
          public DateTime Data { get; set; } 
          public HttpCookie Cookie { get; set; }
     }
}