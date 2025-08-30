using System;
using UnityEngine;

public class SimpleList<T> : ISimpleList<T>
{
    private T[] items;   // Array interno
    private int count;   // Cantidad de elementos actuales

    public SimpleList(int capacity = 1) // Capacidad inicial por defecto
    {
        if (capacity <= 0) capacity = 1;
        items = new T[capacity];
        count = 0;
    }

    // Indexador
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();
            return items[index];
        }
        set
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();
            items[index] = value;
        }
    }

    public int Count => count;

    public void Add(T item)
    {
        EnsureCapacity();
        items[count] = item;
        count++;
    }

    public void AddRange(T[] collection)
    {
        if (collection == null) return;
        foreach (var item in collection)
        {
            Add(item);
        }
    }

    public bool Remove(T item)
    {
        int index = Array.IndexOf(items, item, 0, count);
        if (index >= 0)
        {
            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }
            count--;
            items[count] = default; // Limpia la referencia
            return true;
        }
        return false;
    }

    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            items[i] = default;
        }
        count = 0;
    }

    private void EnsureCapacity()
    {
        if (count >= items.Length)
        {
            T[] newArray = new T[items.Length * 2];
            Array.Copy(items, newArray, count);
            items = newArray;
        }
    }

    public override string ToString()
    {
        if (count == 0) return "(vacía)";
        string[] arrStr = new string[count];
        for (int i = 0; i < count; i++)
        {
            arrStr[i] = items[i]?.ToString();
        }
        return string.Join(", ", arrStr);
    }
}
