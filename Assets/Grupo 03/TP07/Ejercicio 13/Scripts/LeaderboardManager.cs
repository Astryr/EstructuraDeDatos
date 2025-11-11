using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI leaderboardText;
    public Button addScoreButton;
    public Button preOrderButton;
    public Button inOrderButton;
    public Button postOrderButton;
    public Button levelOrderButton;

    private AVLTree tree = new AVLTree();
    private System.Random rng = new System.Random();

    void Start()
    {
        // CREA 100 SCORES RANDOM PARA POBLAR EL ARBOL
        for (int i = 0; i < 100; i++)
        {
            int score = rng.Next(0, 1000);
            string name = "Jugador " + (i + 1);
            tree.Insert(score, name);
        }

        // INICIALIZA LA UI
        UpdateLeaderboardUI(tree.InOrder(), "LEADERBOARD (RANDOM)");

        // BOTONES
        addScoreButton.onClick.AddListener(AddRandomScore);
        preOrderButton.onClick.AddListener(() => ShowTraversal("Pre-Order"));
        inOrderButton.onClick.AddListener(() => ShowTraversal("In-Order"));
        postOrderButton.onClick.AddListener(() => ShowTraversal("Post-Order"));
        levelOrderButton.onClick.AddListener(() => ShowTraversal("Level-Order"));
    }

    void AddRandomScore()
    {
        int score = rng.Next(0, 1000);
        string name = "Nuevo Jugador " + rng.Next(1000, 9999);
        tree.Insert(score, name);
        Debug.Log($"Se añadio a: {name} con un puntaje de: {score}");

        UpdateLeaderboardUI(tree.InOrder(), "LEADERBOARD (RANDOM)");
    }

    void ShowTraversal(string type)
    {
        List<(string, int)> traversal = new List<(string, int)>();

        switch (type)
        {
            case "Pre-Order":
                traversal = tree.PreOrderList();
                break;
            case "In-Order":
                traversal = tree.InOrder();
                break;
            case "Post-Order":
                traversal = tree.PostOrderList();
                break;
            case "Level-Order":
                traversal = tree.LevelOrderList();
                break;
        }

        UpdateLeaderboardUI(traversal, $"{type} Traversal");
    }

    void UpdateLeaderboardUI(List<(string, int)> scores, string title)
    {
        leaderboardText.text = $"{title}\n\n";
        foreach (var s in scores)
        {
            leaderboardText.text += $"{s.Item1}: {s.Item2}\n";
        }
    }
}
