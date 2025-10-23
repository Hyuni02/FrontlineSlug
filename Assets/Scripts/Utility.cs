using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utility
{
    public static T SelectRandom<T>(List<T> list) {
        if (list.Count == 0) {
            throw new NullReferenceException("list length is 0");
        }
        if (list == null) {
            throw new NullReferenceException("list is null");
        }
        int index = UnityEngine.Random.Range(0, list.Count);
        T select = list[index];
        return select;
    }

    public static T SelectRandom<T>(T[] arr) {
        return SelectRandom(arr.ToList());
    }

    public static T Pop<T>(ref List<T> list, bool last = false) {
        if (list.Count == 0) {
            throw new NullReferenceException("list length is 0");
        }
        if (list == null) {
            throw new NullReferenceException("list is null");
        }
        T pop = list[0];
        if (last) {
            pop = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
        }
        else {
            list.RemoveAt(0);
        }
        return pop;
    }
}
