using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
//Jack
/// <summary>
/// Contains the three commonly-used methods when working with cookies
/// </summary>
public static class CookieUtils
{

    /// <summary>
    /// Checks to see if the browser is supporting session cookies (by seeing if the ASP.NET_sessionId cookie exists)
    /// </summary>
    /// <returns></returns>
    public static bool IsSupportingSessionCookies()
    {
        return (!(HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] == null));
    }

    /// <summary>
    /// Encodes a string representing the cookie for compact and secure storage
    /// </summary>
    /// <param name="s">The string that represents the cookie</param>
    /// <returns>The encoded cookie</returns>
    public static string ToEncodedCookie(string s)
    {
        return HttpUtility.UrlEncode(Convert.ToBase64String(Encoding.UTF8.GetBytes(s)));
    }

    /// <summary>
    /// Decodes a string representing the cookie
    /// </summary>
    /// <param name="s">The string to decode</param>
    /// <returns>The decoded cookie</returns>
    public static string FromEncodedCookie(string s)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(HttpUtility.UrlDecode(s)));
    }
}