using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphUIController : MonoBehaviour
{
    public TMP_InputField vertexInput;
    public TMP_InputField edgeToInput;
    public TMP_InputField weightInput;
    public Button addVertexButton;
    public Button addEdgeButton;
    public TextMeshProUGUI resultText;

    private MyALGraph<string> graph = new();

    void Start()
    {
        addVertexButton.onClick.AddListener(AddVertex);
        addEdgeButton.onClick.AddListener(AddEdge);
    }

    void AddVertex()
    {
        string vertex = vertexInput.text;
        if (string.IsNullOrEmpty(vertex)) return;

        graph.AddVertex(vertex);
        resultText.text = $"Vértice '{vertex}' agregado.";
    }

    void AddEdge()
    {
        string from = vertexInput.text;
        string to = edgeToInput.text;
        if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to)) return;

        if (int.TryParse(weightInput.text, out int weight))
        {
            graph.AddEdge(from, (to, weight));
            resultText.text = $"Arista '{from} → {to}' con peso {weight} agregada.";
        }
        else
        {
            resultText.text = "Peso inválido.";
        }
    }
}