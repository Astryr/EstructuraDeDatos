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

    public void BubbleSort()
    {
        for (int i = 0; i < count - 1; i++)
        {
            for (int j = 0; j < count - i - 1; j++)
            {
                if (((IComparable<T>)items[j]).CompareTo(items[j + 1]) > 0)
                {
                    T temp = items[j];
                    items[j] = items[j + 1];
                    items[j + 1] = temp;
                }
            }
        }
    }

    public void SelectionSort()
    {
        for (int i = 0; i < count - 1; i++)
        {
            int minIdx = i;
            for (int j = i + 1; j < count; j++)
            {
                if (((IComparable<T>)items[j]).CompareTo(items[minIdx]) < 0)
                    minIdx = j;
            }
            if (minIdx != i)
            {
                T temp = items[i];
                items[i] = items[minIdx];
                items[minIdx] = temp;
            }
        }
    }

    public void InsertionSort()
    {
        for (int i = 1; i < count; i++)
        {
            T key = items[i];
            int j = i - 1;
            while (j >= 0 && ((IComparable<T>)items[j]).CompareTo(key) > 0)
            {
                items[j + 1] = items[j];
                j--;
            }
            items[j + 1] = key;
        }
    }

}
