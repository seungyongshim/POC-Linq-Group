using System;
using System.Linq;
using System.Collections.Generic;

var persons = new List<Person>()
{
    new Person("hong", new List<Photo>
    {
        new Photo(1),
        new Photo(2),
        new Photo(3),
    }),
    new Person("hwang", new List<Photo>
    {
        new Photo(12),
        new Photo(22),
        new Photo(32),
    }),
};

var query = from person in persons
            from photo in person.Photos
            from photoWillDispose in photo.Use()
            where photoWillDispose.Id != 22
            select photoWillDispose;

foreach (var item in query)
{
    Console.WriteLine(item);
}


public record Person(string Name, List<Photo> Photos);

public record Photo(int Id) : IDisposable
{
    public void Dispose() { }
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
            Console.WriteLine($"Disposed {obj}");
            obj?.Dispose();
        }
    }
}
