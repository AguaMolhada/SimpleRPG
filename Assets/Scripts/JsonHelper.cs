// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonHelper.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string jsonArray)
    {
        jsonArray = WrapArray(jsonArray);
        return FromJsonWrapped<T>(jsonArray);
    }

    public static T[] FromJsonWrapped<T>(string jsonObject)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(jsonObject);
        return wrapper.items;
    }

    private static string WrapArray(string jsonArray)
    {
        return "{ \"items\": " + jsonArray + "}";
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
}