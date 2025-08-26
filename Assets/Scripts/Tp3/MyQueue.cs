using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyQueue<T>
{
    private List<T> items = new List<T>();

    public int Count => items.Count;

    public void Enqueue(T item) => items.Add(item);

    public T Dequeue()
    {
        if (items.Count == 0) throw new InvalidOperationException("Queue is empty.");
        T item = items[0];
        items.RemoveAt(0);
        return item;
    }

    public T Peek()
    {
        if (items.Count == 0) throw new InvalidOperationException("Queue is empty.");
        return items[0];
    }

    public void Clear() => items.Clear();

    public T[] ToArray() => items.ToArray();

    public override string ToString() => string.Join(", ", items);

    public bool TryDequeue(out T item)
    {
        if (items.Count > 0)
        {
            item = items[0];
            items.RemoveAt(0);
            return true;
        }
        item = default;
        return false;
    }

    public bool TryPeek(out T item)
    {
        if (items.Count > 0)
        {
            item = items[0];
            return true;
        }
        item = default;
        return false;
    }
}

