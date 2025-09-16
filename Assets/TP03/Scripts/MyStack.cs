using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyStack<T>
{
    private SimpleList<T> items = new SimpleList<T>();

    public int Count => items.Count;

    public void Push(T item) => items.Add(item);

    public T Pop()
    {
        if (items.Count == 0) throw new InvalidOperationException("Stack is empty.");
        T item = items[items.Count - 1];
        items.Remove(item);
        return item;
    }

    public T Peek()
    {
        if (items.Count == 0) throw new InvalidOperationException("Stack is empty.");
        return items[items.Count - 1];
    }

    public void Clear() => items.Clear();

    public T[] ToArray()
    {
        T[] array = new T[items.Count];
        for (int i = 0; i < items.Count; i++)
            array[i] = items[i];
        return array;
    }

    public override string ToString()
    {
        return items.ToString();
    }

    public bool TryPop(out T item)
    {
        if (items.Count > 0)
        {
            item = items[items.Count - 1];
            items.Remove(item);
            return true;
        }
        item = default;
        return false;
    }

    public bool TryPeek(out T item)
    {
        if (items.Count > 0)
        {
            item = items[items.Count - 1];
            return true;
        }
        item = default;
        return false;
    }
}
