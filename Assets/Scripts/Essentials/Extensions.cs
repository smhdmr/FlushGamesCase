using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T RandomItem<T>(this IList<T> list)
    {
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list!");
        return list[Random.Range(0, list.Count)];
    }
    
    public static Transform GetLastChild(this Transform t)
    {
        if (t.childCount == 0) return null;
        return t.GetChild(t.childCount - 1);
    }
}
