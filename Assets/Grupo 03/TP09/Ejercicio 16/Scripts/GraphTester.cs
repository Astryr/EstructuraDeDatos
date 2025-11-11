using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphTester : MonoBehaviour
{
    private MyALGraph<string> graph;

    void Start()
    {
        graph = new MyALGraph<string>();

        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddEdge("A", ("B", 10));
        graph.AddEdge("B", ("A", 5));

        Debug.Log("Vertices:");
        foreach (var v in graph.Vertices)
            Debug.Log(v);

        Debug.Log($"¿Existe arista A→B? {graph.ContainsEdge("A", "B")}");
        Debug.Log($"Peso A→B: {graph.GetWeight("A", "B")}");
    }
}
