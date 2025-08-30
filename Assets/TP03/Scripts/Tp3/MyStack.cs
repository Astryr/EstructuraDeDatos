using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyStack<T>
{
    private List<T> items = new List<T>();

    public int Count => items.Count;

    public void Push(T item) => items.Add(item);

    public T Pop()
    {
        if (items.Count == 0) throw new InvalidOperationException("Stack is empty.");
        T item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return item;
    }

    public T Peek()
    {
        if (items.Count == 0) throw new InvalidOperationException("Stack is empty.");
        return items[items.Count - 1];
    }

    public void Clear() => items.Clear();

    public T[] ToArray() => items.ToArray();

    public override string ToString() => string.Join(", ", items);

    public bool TryPop(out T item)
    {
        if (items.Count > 0)
        {
            item = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
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
