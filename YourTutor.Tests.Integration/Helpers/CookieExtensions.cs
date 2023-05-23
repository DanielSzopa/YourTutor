using System.Net;

namespace YourTutor.Tests.Integration.Helpers;

public static class CookieExtensions
{
    public static CookieCollection GetAllCookies(this HttpResponseMessage response)
    {
        var mockUri = new Uri("http://mockuri");
        CookieContainer cookies = new CookieContainer();
        var headers = default(IEnumerable<string>);
        var result = response.Headers.TryGetValues("Set-Cookie", out headers);

        if (!result)
            return new CookieCollection();

        foreach (var header in headers)
        {
            cookies.SetCookies(mockUri, header);
        }

        return cookies.GetAllCookies();
    }

    public static bool ContainsCookie(this HttpResponseMessage response, string name)
    {
        var cookies = GetAllCookies(response);
        if (cookies.Count <= 0)
            return false;

        var cookie = cookies.SingleOrDefault(x => x.Name.ToLower() == name.ToLower());

        return cookie != null ? true : false;
    }
}
