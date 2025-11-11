using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetSelector : MonoBehaviour
{
    public static PlanetSelector Instance;
    public TextMeshProUGUI resultText;

    private List<PlanetNode> selectedPlanets = new();
    public MyALGraph<string> graph;

    private void Awake()
    {
        Instance = this;
        graph = new MyALGraph<string>();
        SetupGraph();
    }

    public void SelectPlanet(PlanetNode planet)
    {
        if (!selectedPlanets.Contains(planet))
        {
            selectedPlanets.Add(planet);
            planet.MarkSelected();
        }
    }

    public void CheckPath()
    {
        if (selectedPlanets.Count == 0)
        {
            resultText.text = "No hay ningún planeta seleccionado.";
            return;
        }

        int totalCost = 0;
        bool valid = true;
        List<string> recorrido = new();

        for (int i = 0; i < selectedPlanets.Count - 1; i++)
        {
            var from = selectedPlanets[i].planetName;
            var to = selectedPlanets[i + 1].planetName;

            recorrido.Add(from);

            if (graph.ContainsEdge(from, to))
            {
                totalCost += graph.GetWeight(from, to) ?? 0;
            }
            else
            {
                valid = false;
                break;
            }
        }

        recorrido.Add(selectedPlanets[^1].planetName); // último planeta

        if (valid)
            resultText.text = $"Camino válido: {string.Join(" → ", recorrido)}\nCosto total: {totalCost}";
        else
            resultText.text = "Camino inválido.";

        foreach (var planet in selectedPlanets)
            planet.Unmark();

        selectedPlanets.Clear();
    }

    private void SetupGraph()
    {
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
        graph.AddEdge("Tierra", ("Marte", GetDistance(tierra, marte)));
        graph.AddEdge("Marte", ("Jupiter", GetDistance(marte, jupiter)));
        graph.AddEdge("Jupiter", ("Saturno", GetDistance(jupiter, saturno)));
        graph.AddEdge("Saturno", ("Urano", GetDistance(saturno, urano)));
        graph.AddEdge("Urano", ("Neptuno", GetDistance(urano, neptuno)));
        graph.AddEdge("Venus", ("Tierra", GetDistance(venus, tierra)));
        graph.AddEdge("Pluton", ("Neptuno", GetDistance(pluton, neptuno)));
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
        return Mathf.RoundToInt(Vector2.Distance(a.transform.position, b.transform.position));
    }
}

