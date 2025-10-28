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
            selectedPlanets.Add(planet);
    }

    public void CheckPath()
    {
        int totalCost = 0;
        bool valid = true;

        for (int i = 0; i < selectedPlanets.Count - 1; i++)
        {
            var from = selectedPlanets[i].planetName;
            var to = selectedPlanets[i + 1].planetName;

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

        resultText.text = valid ? $"Camino válido. Costo total: {totalCost}" : "Camino inválido.";
        selectedPlanets.Clear();
    }

    private void SetupGraph()
    {
        // Ejemplo de inicialización
        graph.AddVertex("Tierra");
        graph.AddVertex("Marte");
        graph.AddVertex("Júpiter");

        graph.AddEdge("Tierra", ("Marte", 50));
        graph.AddEdge("Marte", ("Júpiter", 70));
    }
}
