using System;
using System.Collections.Generic;
using UnityEngine;

public class MySetArray<T> : MySet<T>
{
    private T[] elements;
    private int count;
    private const int DEFAULT_CAPACITY = 10; 

    
    public override T[] Elements
    {
        get
        {
            
            T[] actualElements = new T[count];
            Array.Copy(elements, actualElements, count);
            return actualElements;
        }
    }

    public MySetArray()
    {
        elements = new T[DEFAULT_CAPACITY];
        count = 0;
    }

    public override void Add(T item)
    {
        if (Contains(item)) return; 

        if (count >= elements.Length)
        {
            Array.Resize(ref elements, elements.Length * 2);
        }
        elements[count++] = item;
    }

    public override void Remove(T item)
    {
        for (int i = 0; i < count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(elements[i], item))
            {
                
                elements[i] = elements[count - 1];
                count--;
                return;
            }
        }
    }

    public override void Clear()
    {
        count = 0;
        elements = new T[DEFAULT_CAPACITY];
    }

    public override bool Contains(T item)
    {
        for (int i = 0; i < count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(elements[i], item))
                return true;
        }
        return false;
    }

    public override void Show()
    {
        Debug.Log(ToString());
    }

    public override string ToString()
    {
        return "{ " + string.Join(", ", Elements) + " }";
    }

    public override int Cardinality()
    {
        return count;
    }

    public override bool IsEmpty()
    {
        return count == 0;
    }

    public override MySet<T> Union(MySet<T> other)
    {
        MySetArray<T> result = new MySetArray<T>();

        
        foreach (var e in this.Elements)
            result.Add(e);

        
        foreach (var e in other.Elements)
            result.Add(e);

        return result;
    }

    public override MySet<T> Intersect(MySet<T> other)
    {
        MySetArray<T> result = new MySetArray<T>();
        
        for (int i = 0; i < count; i++)
        {
            if (other.Contains(elements[i]))
                result.Add(elements[i]);
        }
        return result;
    }

    public override MySet<T> Difference(MySet<T> other)
    {
        MySetArray<T> result = new MySetArray<T>();
       
        for (int i = 0; i < count; i++)
        {
            if (!other.Contains(elements[i]))
                result.Add(elements[i]);
        }
        return result;
    }
}