using System.Collections.Generic;
using System.Linq;

namespace SVB.Ticker.Server.Common.System.Extension
{
  /// <summary>
  /// Static class that provides various extension methods for collections.
  /// </summary>
  public static class CollectionExtensions
  {
    /// <summary>
    /// Adds all elements in <code>elementsToAdd</code> to the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    /// <param name="elementsToAdd"></param>
    public static void AddAll<T>(this ICollection<T> instance, IEnumerable<T> elementsToAdd)
    {
      if (elementsToAdd == null)
        return;

      foreach (T element in elementsToAdd)
        instance.Add(element);
    }

    /// <summary>
    /// Adds all elements in <code>elementsToAdd</code> to the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    /// <param name="elementsToAdd"></param>
    public static void AddAll<T>(this ICollection<T> instance, params T[] elementsToAdd)
    {
      AddAll(instance, elementsToAdd as IEnumerable<T>);
    }

    /// <summary>
    /// Removes all elements in <code>elementsToRemove</code> from the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    /// <param name="elementsToRemove"></param>
    public static void RemoveAll<T>(this ICollection<T> instance, IEnumerable<T> elementsToRemove)
    {
      if (elementsToRemove == null)
        return;

      foreach (T element in elementsToRemove.Where(instance.Contains))
        instance.Remove(element);
    }

    /// <summary>
    /// Removes all elements in <code>elementsToRemove</code> from the collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    /// <param name="elementsToRemove"></param>
    public static void RemoveAll<T>(this ICollection<T> instance, params T[] elementsToRemove)
    {
      RemoveAll(instance, elementsToRemove as IEnumerable<T>);
    }
  }
}