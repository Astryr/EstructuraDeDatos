using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PlanetSelector : MonoBehaviour
{
    public static PlanetSelector Instance;
    public TextMeshProUGUI resultText;

    [Header("Configuración de Líneas")]
    public Material lineMaterial; 
    public Color defaultLineColor = new Color(1, 1, 1, 0.2f); 
    public Color highlightLineColor = Color.green; 
    public float lineWidth = 0.1f;

    public MyALGraph<string> graph;
    private List<PlanetNode> selectedPlanets = new();

    
    private List<(string, string)> allEdges = new();
    private List<GameObject> highlightLines = new();

    private void Awake()
    {
        Instance = this;
        
    }

    
    public void SetupAndDrawGraph()
    {
        graph = new MyALGraph<string>();
        allEdges.Clear(); 

        // Agregar vértices
        graph.AddVertex("Tierra");
        graph.AddVertex("Marte");
        graph.AddVertex("Saturno");
        graph.AddVertex("Neptuno");
        graph.AddVertex("Urano");
        graph.AddVertex("Venus");
        graph.AddVertex("Pluton");
        graph.AddVertex("Jupiter");

        // Obtener referencias a los planetas en escena
        PlanetNode tierra = FindPlanet("Tierra");
        PlanetNode marte = FindPlanet("Marte");
        PlanetNode saturno = FindPlanet("Saturno");
        PlanetNode neptuno = FindPlanet("Neptuno");
        PlanetNode urano = FindPlanet("Urano");
        PlanetNode venus = FindPlanet("Venus");
        PlanetNode pluton = FindPlanet("Pluton");
        PlanetNode jupiter = FindPlanet("Jupiter");

        // Agregar aristas con distancia real como peso
        AddEdgeWithLine(tierra, marte);
        AddEdgeWithLine(marte, jupiter);
        AddEdgeWithLine(jupiter, saturno);
        AddEdgeWithLine(saturno, urano);
        AddEdgeWithLine(urano, neptuno);
        AddEdgeWithLine(venus, tierra);
        AddEdgeWithLine(pluton, neptuno);

        // Dibujar todas las aristas del grafo
        DrawAllGraphEdges();
    }

    
    private void AddEdgeWithLine(PlanetNode from, PlanetNode to)
    {
        if (from == null || to == null) return;

        int distance = GetDistance(from, to);
        graph.AddEdge(from.planetName, (to.planetName, distance));
        allEdges.Add((from.planetName, to.planetName));
    }

    
    private void DrawAllGraphEdges()
    {
        foreach (var edge in allEdges)
        {
            PlanetNode from = FindPlanet(edge.Item1);
            PlanetNode to = FindPlanet(edge.Item2);
            if (from != null && to != null)
            {
                CreateLine(from.transform.position, to.transform.position, defaultLineColor);
            }
        }
    }

    public void SelectPlanet(PlanetNode planet)
    {
        
        ClearHighlightLines();

        if (!selectedPlanets.Contains(planet))
        {
            selectedPlanets.Add(planet);
            planet.MarkSelected();
        }
    }

    public void CheckPath()
    {
        
        ClearHighlightLines();

        if (selectedPlanets.Count < 2) 
        {
            resultText.text = "Selecciona al menos 2 planetas.";
            ClearSelection();
            return;
        }

        int totalCost = 0;
        bool valid = true;
        List<string> recorrido = new List<string>();

        for (int i = 0; i < selectedPlanets.Count - 1; i++)
        {
            var fromNode = selectedPlanets[i];
            var toNode = selectedPlanets[i + 1];

            recorrido.Add(fromNode.planetName);

            if (graph.ContainsEdge(fromNode.planetName, toNode.planetName))
            {
                totalCost += graph.GetWeight(fromNode.planetName, toNode.planetName) ?? 0;

                
                GameObject line = CreateLine(fromNode.transform.position, toNode.transform.position, highlightLineColor);
                highlightLines.Add(line); 
            }
            else
            {
                valid = false;
                break;
            }
        }

        recorrido.Add(selectedPlanets.Last().planetName); 

        if (valid)
            resultText.text = $"Camino válido: {string.Join(" → ", recorrido)}\nCosto total: {totalCost}";
        else
            resultText.text = $"Camino inválido en {recorrido.Last()}.";

        ClearSelection();
    }

    
    private void ClearSelection()
    {
        foreach (var planet in selectedPlanets)
            planet.Unmark();
        selectedPlanets.Clear();
    }

    private void ClearHighlightLines()
    {
        foreach (var line in highlightLines)
            Destroy(line);
        highlightLines.Clear();
    }

    private PlanetNode FindPlanet(string name)
    {
        foreach (PlanetNode node in FindObjectsByType<PlanetNode>(FindObjectsSortMode.None))
        {
            if (node.planetName == name)
                return node;
        }
        Debug.LogWarning($"No se encontró el planeta: {name}");
        return null;
    }

    private int GetDistance(PlanetNode a, PlanetNode b)
    {
        if (a == null || b == null) return 0;
        
        return Mathf.RoundToInt(Vector2.Distance(
            a.GetComponent<RectTransform>().anchoredPosition,
            b.GetComponent<RectTransform>().anchoredPosition
        ));
    }

    
    private GameObject CreateLine(Vector3 from, Vector3 to, Color color)
    {
        GameObject lineObj = new GameObject($"Line_{from}_{to}");
        lineObj.transform.SetParent(this.transform); 

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.SetPosition(0, from);
        lr.SetPosition(1, to);

        
        lr.useWorldSpace = false;

        return lineObj;
    }
}