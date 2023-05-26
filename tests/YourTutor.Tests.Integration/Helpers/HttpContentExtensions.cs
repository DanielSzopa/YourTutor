using System.Reflection;

namespace YourTutor.Tests.Integration.Helpers;

public static class HttpContentExtensions
{
    public static FormUrlEncodedContent ToFormContent<T>(this T obj)
        where T : class
    {
        var formValues = new List<KeyValuePair<string, string>>();

        Type type = obj.GetType();
        var props = new List<PropertyInfo>(type.GetProperties());

        foreach (var prop in props)
        {
            string propValue = prop.GetValue(obj)?.ToString();
            formValues.Add(new KeyValuePair<string, string>(prop.Name?.ToString(), propValue));
        }

        return new FormUrlEncodedContent(formValues);
    }
}
