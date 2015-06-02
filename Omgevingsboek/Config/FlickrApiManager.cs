using FlickrNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Omgevingsboek.Config
{
    public class FlickrApiManager
    {
        public static Flickr GetInstance()
        {
            return new Flickr(ConfigurationManager.AppSettings.Get("FlickrApiKey"), ConfigurationManager.AppSettings.Get("FlickrSharedSecret"));

        }
        public static void setCookie(OAuthAccessToken value)
        {
            HttpCookie cookie = new HttpCookie("OAuthToken");
            cookie.Expires = DateTime.UtcNow.AddHours(1);
            cookie.Values["FullName"] = value.FullName;
            cookie.Values["Token"] = value.Token;
            cookie.Values["TokenSecret"] = value.TokenSecret;
            cookie.Values["UserId"] = value.UserId;
            cookie.Values["Username"] = value.Username;
            HttpContext.Current.Response.AppendCookie(cookie);
        }
    }
    
}