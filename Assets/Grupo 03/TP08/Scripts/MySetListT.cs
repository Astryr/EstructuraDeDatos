using System.Collections.Generic;
using UnityEngine;
public class MySetList<T> : MySet<T>
{
    private List<T> elements = new List<T>();

    
    public override T[] Elements
    {
        get
        {
            return elements.ToArray(); 
        }
    }

    public override void Add(T item)
    {
         if (!elements.Contains(item)) 
            elements.Add(item);
    }

    public override void Remove(T item)
    {
        elements.Remove(item);
    }

    public override void Clear()
    {
        elements.Clear();
    }

    public override bool Contains(T item)
    {
        return elements.Contains(item);
    }

    public override void Show()
    {
        Debug.Log(ToString());
    }

    public override string ToString()
    {
        return "{ " + string.Join(", ", elements) + " }";
    }

    public override int Cardinality()
    {
        return elements.Count;
    }

    public override bool IsEmpty()
    {
        return elements.Count == 0;
    }

    public override MySet<T> Union(MySet<T> other)
    {
        MySetList<T> result = new MySetList<T>();

        
        foreach (var e in this.elements)
            result.Add(e);

        
        foreach (var e in other.Elements)
            result.Add(e);

        return result;
    }

    public override MySet<T> Intersect(MySet<T> other)
    {
        MySetList<T> result = new MySetList<T>();
        
        foreach (var e in elements)
        {
            if (other.Contains(e))
                result.Add(e);
        }
        return result;
    }

    public override MySet<T> Difference(MySet<T> other)
    {
        MySetList<T> result = new MySetList<T>();
        
        foreach (var e in elements)
        {
            if (!other.Contains(e))
                result.Add(e);
        }
        return result;
    }
}
