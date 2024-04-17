using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public static class EventLink
{

    private static GenericDictionary<string> events = new GenericDictionary<string>();



    public static void SetAction(string identifier, Signature action)
    {
        events.Set<GenericEvent<Signature>>(identifier, new GenericEvent<Signature>(action, true));
    }

    public static void SetAction<T>(string identifier, Signature<T> action)
    {
        events.Set<GenericEvent<Signature<T>>>(identifier, new GenericEvent<Signature<T>>(action, true));
    }

    public static void SetAction<T1, T2>(string identifier, Signature<T1, T2> action)
    {
        events.Set<GenericEvent<Signature<T1, T2>>>(identifier, new GenericEvent<Signature<T1, T2>>(action, true));
    }

    public static void SetAction<T1, T2, T3>(string identifier, Signature<T1, T2, T3> action)
    {
        events.Set<GenericEvent<Signature<T1, T2, T3>>>(identifier, new GenericEvent<Signature<T1, T2, T3>>(action, true));
    }

    public static void SetAction<T1, T2, T3, T4>(string identifier, Signature<T1, T2, T3, T4> action)
    {
        events.Set<GenericEvent<Signature<T1, T2, T3, T4>>>(identifier, new GenericEvent<Signature<T1, T2, T3, T4>>(action, true));
    }

    public static bool InvokeAction(string identifier)
    {
        GenericEvent<Signature> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (value.isAction)
            {
                value.action.Invoke();
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool InvokeAction<T>(string identifier, T paramater)
    {
        GenericEvent<Signature<T>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (value.isAction)
            {
                value.action.Invoke(paramater);
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool InvokeAction<T1, T2>(string identifier, T1 p1, T2 p2)
    {
        GenericEvent<Signature<T1, T2>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (value.isAction)
            {
                value.action.Invoke(p1, p2);
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool InvokeAction<T1, T2, T3>(string identifier, T1 p1, T2 p2, T3 p3)
    {
        GenericEvent<Signature<T1, T2, T3>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (value.isAction)
            {
                value.action.Invoke(p1, p2, p3);
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool InvokeAction<T1, T2, T3, T4>(string identifier, T1 p1, T2 p2, T3 p3, T4 p4)
    {
        GenericEvent<Signature<T1, T2, T3, T4>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (value.isAction)
            {
                value.action.Invoke(p1, p2, p3, p4);
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool DoesActionExist(string identifier)
    {
        if (events.ContainsKey<GenericEvent<Signature>>(identifier))
        {
            if (((GenericEvent<Signature>)events.dictionaries[typeof(GenericEvent<Signature>)][identifier]).isAction)
            {
                return true;
            }
        }
        return false;
    }

    public static bool DoesActionExist<T>(string identifier)
    {
        if (events.ContainsKey<GenericEvent<Signature<T>>>(identifier))
        {
            if (((GenericEvent<Signature<T>>)events.dictionaries[typeof(GenericEvent<Signature<T>>)][identifier]).isAction)
            {
                return true;
            }
        }
        return false;
    }

    public static bool DoesActionExist<T1, T2>(string identifier)
    {
        if (events.ContainsKey<GenericEvent<Signature<T1, T2>>>(identifier))
        {
            if (((GenericEvent<Signature<T1, T2>>)events.dictionaries[typeof(GenericEvent<Signature<T1, T2>>)][identifier]).isAction)
            {
                return true;
            }
        }
        return false;
    }

    public static bool DoesActionExist<T1, T2, T3>(string identifier)
    {
        if (events.ContainsKey<GenericEvent<Signature<T1, T2, T3>>>(identifier))
        {
            if (((GenericEvent<Signature<T1, T2, T3>>)events.dictionaries[typeof(GenericEvent<Signature<T1, T2, T3>>)][identifier]).isAction)
            {
                return true;
            }
        }
        return false;
    }

    public static bool DoesActionExist<T1, T2, T3, T4>(string identifier)
    {
        if (events.ContainsKey<GenericEvent<Signature<T1, T2, T3, T4>>>(identifier))
        {
            if (((GenericEvent<Signature<T1, T2, T3, T4>>)events.dictionaries[typeof(GenericEvent<Signature<T1, T2, T3, T4>>)][identifier]).isAction)
            {
                return true;
            }
        }
        return false;
    }



    public static void SetEvent(string identifier, Signature action = default)
    {
        events.Set<GenericEvent<Signature>>(identifier, new GenericEvent<Signature>(action, false));
    }

    public static void SetEvent<T>(string identifier, Signature<T> action = default)
    {
        events.Set<GenericEvent<Signature<T>>>(identifier, new GenericEvent<Signature<T>>(action, false));
    }

    public static void SetEvent<T1, T2>(string identifier, Signature<T1, T2> action = default)
    {
        events.Set<GenericEvent<Signature<T1, T2>>>(identifier, new GenericEvent<Signature<T1, T2>>(action, false));
    }

    public static void SetEvent<T1, T2, T3>(string identifier, Signature<T1, T2, T3> action = default)
    {
        events.Set<GenericEvent<Signature<T1, T2, T3>>>(identifier, new GenericEvent<Signature<T1, T2, T3>>(action, false));
    }

    public static void SetEvent<T1, T2, T3, T4>(string identifier, Signature<T1, T2, T3, T4> action = default)
    {
        events.Set<GenericEvent<Signature<T1, T2, T3, T4>>>(identifier, new GenericEvent<Signature<T1, T2, T3, T4>>(action, false));
    }

    public static bool InvokeEvent(string identifier)
    {
        GenericEvent<Signature> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction && value.action != null)
            {
                value.action.Invoke();
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool InvokeEvent<T>(string identifier, T paramater)
    {
        GenericEvent<Signature<T>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction && value.action != null)
            {
                value.action.Invoke(paramater);
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool InvokeEvent<T1, T2>(string identifier, T1 p1, T2 p2)
    {
        GenericEvent<Signature<T1, T2>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction && value.action != null)
            {
                value.action.Invoke(p1, p2);
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool InvokeEvent<T1, T2, T3>(string identifier, T1 p1, T2 p2, T3 p3)
    {
        GenericEvent<Signature<T1, T2, T3>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction && value.action != null)
            {
                value.action.Invoke(p1, p2, p3);
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool InvokeEvent<T1, T2, T3, T4>(string identifier, T1 p1, T2 p2, T3 p3, T4 p4)
    {
        GenericEvent<Signature<T1, T2, T3, T4>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction && value.action != null)
            {
                value.action.Invoke(p1, p2, p3, p4);
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool ClearEvent(string identifier)
    {
        GenericEvent<Signature> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action = () => { };
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool ClearEvent<T>(string identifier)
    {
        GenericEvent<Signature<T>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action = (T a) => { };
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool ClearEvent<T1, T2>(string identifier)
    {
        GenericEvent<Signature<T1, T2>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action = (T1 a, T2 b) => { };
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool ClearEvent<T1, T2, T3>(string identifier)
    {
        GenericEvent<Signature<T1, T2, T3>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action = (T1 a, T2 b, T3 c) => { };
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool ClearEvent<T1, T2, T3, T4>(string identifier)
    {
        GenericEvent<Signature<T1, T2, T3, T4>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action = (T1 a, T2 b, T3 c, T4 d) => { };
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool AddEventListener(string identifier, Signature action)
    {
        GenericEvent<Signature> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action += action;
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool AddEventListener<T>(string identifier, Signature<T> action)
    {
        GenericEvent<Signature<T>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action += action;
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool AddEventListener<T1, T2>(string identifier, Signature<T1, T2> action)
    {
        GenericEvent<Signature<T1, T2>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action += action;
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool AddEventListener<T1, T2, T3>(string identifier, Signature<T1, T2, T3> action)
    {
        GenericEvent<Signature<T1, T2, T3>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action += action;
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool AddEventListener<T1, T2, T3, T4>(string identifier, Signature<T1, T2, T3, T4> action)
    {
        GenericEvent<Signature<T1, T2, T3, T4>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action += action;
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool RemoveEventListener(string identifier, Signature action)
    {
        GenericEvent<Signature> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action -= action;
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool RemoveEventListener<T>(string identifier, Signature<T> action)
    {
        GenericEvent<Signature<T>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action -= action;
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool RemoveEventListener<T1, T2>(string identifier, Signature<T1, T2> action)
    {
        GenericEvent<Signature<T1, T2>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action -= action;
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool RemoveEventListener<T1, T2, T3>(string identifier, Signature<T1, T2, T3> action)
    {
        GenericEvent<Signature<T1, T2, T3>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action -= action;
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool RemoveEventListener<T1, T2, T3, T4>(string identifier, Signature<T1, T2, T3, T4> action)
    {
        GenericEvent<Signature<T1, T2, T3, T4>> value = default;
        if (events.TryGet(identifier, ref value))
        {
            if (!value.isAction)
            {
                value.action -= action;
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool DoesEventExist(string identifier)
    {
        if (events.ContainsKey<GenericEvent<Signature>>(identifier))
        {
            if (!((GenericEvent<Signature>)events.dictionaries[typeof(GenericEvent<Signature>)][identifier]).isAction)
            {
                return true;
            }
        }
        return false;
    }

    public static bool DoesEventExist<T>(string identifier)
    {
        if (events.ContainsKey<GenericEvent<Signature<T>>>(identifier))
        {
            if (!((GenericEvent<Signature<T>>)events.dictionaries[typeof(GenericEvent<Signature<T>>)][identifier]).isAction)
            {
                return true;
            }
        }
        return false;
    }

    public static bool DoesEventExist<T1, T2>(string identifier)
    {
        if (events.ContainsKey<GenericEvent<Signature<T1, T2>>>(identifier))
        {
            if (!((GenericEvent<Signature<T1, T2>>)events.dictionaries[typeof(GenericEvent<Signature<T1, T2>>)][identifier]).isAction)
            {
                return true;
            }
        }
        return false;
    }

    public static bool DoesEventExist<T1, T2, T3>(string identifier)
    {
        if (events.ContainsKey<GenericEvent<Signature<T1, T2, T3>>>(identifier))
        {
            if (!((GenericEvent<Signature<T1, T2, T3>>)events.dictionaries[typeof(GenericEvent<Signature<T1, T2, T3>>)][identifier]).isAction)
            {
                return true;
            }
        }
        return false;
    }

    public static bool DoesEventExist<T1, T2, T3, T4>(string identifier)
    {
        if (events.ContainsKey<GenericEvent<Signature<T1, T2, T3 ,T4>>>(identifier))
        {
            if (!((GenericEvent<Signature<T1, T2, T3 ,T4>>)events.dictionaries[typeof(GenericEvent<Signature<T1, T2, T3 ,T4>>)][identifier]).isAction)
            {
                return true;
            }
        }
        return false;
    }



    public static Type GetSignature(string identifier)
    {
        foreach (var dictionary in events.dictionaries.Values)
        {
            if (dictionary.ContainsKey(identifier))
            {
                return dictionary[identifier].GetType();
            }
        }

        return null;
    }

    private class GenericEvent<T> where T : Delegate
    {
        public T action;
        public bool isAction;

        public GenericEvent(T action, bool isAction)
        {
            this.action = action;
            this.isAction = isAction;
        }

        public GenericEvent()
        {

        }
    }

    public static void ClearAllEvents()
    {
        events = new GenericDictionary<string>();
    }

    private class GenericDictionary<K>
    {
        public Dictionary<Type, Dictionary<K, object>> dictionaries = new Dictionary<Type, Dictionary<K, object>>();

        public void Set<T>(K key, T value)
        {
            Type type = value.GetType();
            if (!dictionaries.ContainsKey(type))
            {
                dictionaries.Add(type, new Dictionary<K, object>());
            }
            dictionaries[type][key] = value;
        }

        public bool TryGet<T>(K key, ref T value)
        {
            value = default;
            Type type = typeof(T);

            if (dictionaries.ContainsKey(type))
            {
                if (dictionaries[type].ContainsKey(key))
                {
                    value = (T)dictionaries[type][key];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool ContainsKey<T>(K key)
        {
            Type type = typeof(T);

            if (dictionaries.ContainsKey(type))
            {
                if (dictionaries[type].ContainsKey(key))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }

    public delegate void Signature();
    public delegate void Signature<T>(T p);
    public delegate void Signature<T1, T2>(T1 p1, T2 p2);
    public delegate void Signature<T1, T2, T3>(T1 p1, T2 p2, T3 p3);
    public delegate void Signature<T1, T2, T3, T4>(T1 p1, T2 p2, T3 p3, T4 p4);
}