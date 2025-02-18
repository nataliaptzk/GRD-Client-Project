﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains additional extenstion functions.
/// - Natalia Pietrzak
/// </summary>
public static class Extensions
{
    public static bool HasComponent<T>(this GameObject obj) where T : Component
    {
        return obj.GetComponent<T>() != null;
    }


    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}