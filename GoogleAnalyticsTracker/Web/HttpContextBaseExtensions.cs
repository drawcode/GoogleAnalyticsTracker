using System.Web;
//using Newtonsoft.Json;

namespace GoogleAnalyticsTracker.Web
{
    public static class HttpContextBaseExtensions
    {
		
#if !UNITY3D
        public static T GetDeserializedCookieValue<T>(this HttpContextBase context, string key)
        {
            if (context != null)
            {
                var cookie = context.Request.Cookies.Get(key);
                if (cookie != null)
                {
                    //return JsonConvert.DeserializeObject<T>(cookie.Value);
                }
            }
            return default(T);
        }

        public static void SetSerializedCookieValue(this HttpContextBase context, string key, object value)
        {
            if (context != null)
            {
               // context.Response.Cookies.Add(new HttpCookie(key, JsonConvert.SerializeObject(value)));
            }
        }
#else
        public static T GetDeserializedCookieValue<T>(this HttpContext context, string key)
        {
            if (context != null)
            {
                var cookie = context.Request.Cookies.Get(key);
                if (cookie != null)
                {
					// TODO wire in LitJson or just store in system prefs
                    //return JsonConvert.DeserializeObject<T>(cookie.Value);
                }
            }
            return default(T);
        }

        public static void SetSerializedCookieValue(this HttpContext context, string key, object value)
        {
            if (context != null)
            {
				// TODO wire in LitJson or just store in system prefs
               // context.Response.Cookies.Add(new HttpCookie(key, JsonConvert.SerializeObject(value)));
            }
        }
		
#endif
    }
}