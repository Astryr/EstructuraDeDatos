using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyALGraph<T>
{
    private Dictionary<T, List<(T, int)>> adjacencyList = new();

    public IEnumerable<T> Vertices => adjacencyList.Keys;

    public void AddVertex(T vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
            adjacencyList[vertex] = new List<(T, int)>();
    }

    public void RemoveVertex(T vertex)
    {
        if (!adjacencyList.ContainsKey(vertex)) return;

        adjacencyList.Remove(vertex);

        foreach (var list in adjacencyList.Values)
            list.RemoveAll(edge => edge.Item1.Equals(vertex));
    }

    public void AddEdge(T from, (T, int) edge)
    {
        if (!adjacencyList.ContainsKey(from)) AddVertex(from);
        if (!adjacencyList.ContainsKey(edge.Item1)) AddVertex(edge.Item1);

        adjacencyList[from].Add(edge);
    }

    public void RemoveEdge(T from, T to)
    {
        if (!adjacencyList.ContainsKey(from)) return;
        adjacencyList[from].RemoveAll(edge => edge.Item1.Equals(to));
    }

    public bool ContainsVertex(T vertex) => adjacencyList.ContainsKey(vertex);

    public bool ContainsEdge(T from, T to)
    {
        return adjacencyList.ContainsKey(from) &&
               adjacencyList[from].Exists(edge => edge.Item1.Equals(to));
    }

    public int? GetWeight(T from, T to)
    {
        if (!adjacencyList.ContainsKey(from)) return null;
        var edge = adjacencyList[from].Find(e => e.Item1.Equals(to));
        return edge.Equals(default) ? null : edge.Item2;
    }
}

