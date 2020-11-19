using System;
using System.Linq;
using System.Collections.Generic;

var query = from id in Enumerable.Range(1, 10)
            let photo = new Photo(id)
            from photoWillDispose in photo.Use()
            where photoWillDispose.Id != 5 &&
                  photoWillDispose.Id != 7
            select photoWillDispose;

foreach (var item in query)
{
    try
    {
        Console.WriteLine(ExceptFunc(item));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

static Photo ExceptFunc(Photo a) => a.Id switch
{
    3 => throw new Exception($"{a}"),
    _ => a,
};


public record Photo(int Id) : IDisposable
{
    public void Dispose() => Console.WriteLine($"Disposed {this}"); 
}

public static class Extensions
{
    public static IEnumerable<T> Use<T>(this T obj) where T : IDisposable
    {
        try
        {
            yield return obj;
        }
        finally
        {
            obj?.Dispose();
        }
    }
}
