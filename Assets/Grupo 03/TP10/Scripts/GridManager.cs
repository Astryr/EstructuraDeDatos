using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine;
using TMPro; // Asegúrate de que sea TMPro
using System.Collections.Generic;
using System.Collections;

public class GridManager : MonoBehaviour
{
    [Header("Configuración de la Grilla")]
    public GameObject tilePrefab;
    public int gridWidth = 12;
    public int gridHeight = 12;

    [Header("Configuración de Colores")]
    public Color floorColor = Color.white;
    public Color wallColor = Color.black;
    public Color entranceColor = Color.blue;
    public Color exitColor = Color.red;

    [Header("Configuración de la UI")]
    public TextMeshProUGUI solutionText; // Asegúrate de que sea TextMeshProUGUI

    [Header("Configuración del Personaje")]
    public GameObject characterPrefab;
    public float characterSpeed = 2f;

    // --- Variables Internas ---
    private Tile[,] grid;
    private TileType currentTileType = TileType.Floor;
    private Node entryPoint = null;
    private Node exitPoint = null;
    private Pathfinder pathfinder;
    private List<Node> currentPath;
    private GameObject characterInstance;

    void Start()
    {
        pathfinder = new Pathfinder();
        GenerateGrid();
    }

    void Update()
    {
        HandlePaintingInput();
    }

    void GenerateGrid()
    {
        grid = new Tile[gridWidth, gridHeight];
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject newTileObject = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                newTileObject.transform.SetParent(transform);
                newTileObject.name = $"Tile_{x}_{y}";

                Tile tile = newTileObject.GetComponent<Tile>();
                // Inicializamos todos como suelo por defecto
                tile.Initialize(x, y, TileType.Floor);
                tile.SetType(TileType.Floor, floorColor); // Le pasamos el color inicial
                grid[x, y] = tile;
            }
        }
    }

    void HandlePaintingInput()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(mousePos.x);
            int y = Mathf.RoundToInt(mousePos.y);

            if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
            {
                PaintTile(x, y);
            }
        }
    }

    void PaintTile(int x, int y)
    {
        Tile tileToPaint = grid[x, y];
        if (tileToPaint.type == currentTileType) return;

        if (currentTileType == TileType.Entrance)
        {
            if (entryPoint != null)
            {
                grid[entryPoint.X, entryPoint.Y].SetType(TileType.Floor, floorColor);
            }
            entryPoint = new Node(x, y);
        }
        else if (currentTileType == TileType.Exit)
        {
            if (exitPoint != null)
            {
                grid[exitPoint.X, exitPoint.Y].SetType(TileType.Floor, floorColor);
            }
            exitPoint = new Node(x, y);
        }

        if (tileToPaint.type == TileType.Entrance) entryPoint = null;
        if (tileToPaint.type == TileType.Exit) exitPoint = null;

        // Pasa el tipo Y el color correspondiente al tile
        tileToPaint.SetType(currentTileType, GetColorForType(currentTileType));
    }

    // Helper para obtener el color correcto
    Color GetColorForType(TileType type)
    {
        switch (type)
        {
            case TileType.Entrance: return entranceColor;
            case TileType.Exit: return exitColor;
            case TileType.Wall: return wallColor;
            case TileType.Floor:
            default: return floorColor;
        }
    }

    // --- Métodos de UI (sin cambios) ---
    public void SelectEntrance() => currentTileType = TileType.Entrance;
    public void SelectExit() => currentTileType = TileType.Exit;
    public void SelectFloor() => currentTileType = TileType.Floor;
    public void SelectWall() => currentTileType = TileType.Wall;
    public void CheckForSolution()
    {
        if (entryPoint == null || exitPoint == null)
        {
            solutionText.text = "Error: Debes colocar una ENTRADA y una SALIDA.";
            return;
        }
        currentPath = pathfinder.FindPath(grid, entryPoint, exitPoint);
        if (currentPath != null)
        {
            solutionText.text = "¡Laberinto con solución!";
        }
        else
        {
            solutionText.text = "El laberinto no tiene solución.";
        }
    }
    public void StartCharacterMovement()
    {
        if (currentPath != null)
        {
            if (characterInstance != null) Destroy(characterInstance);
            characterInstance = Instantiate(characterPrefab, new Vector3(entryPoint.X, entryPoint.Y, -1), Quaternion.identity);
            StartCoroutine(MoveCharacterCoroutine());
        }
        else
        {
            solutionText.text = "No se ha encontrado un camino para recorrer.";
        }
    }
    IEnumerator MoveCharacterCoroutine()
    {
        foreach (Node node in currentPath)
        {
            Vector3 targetPosition = new Vector3(node.X, node.Y, -1);
            while (Vector3.Distance(characterInstance.transform.position, targetPosition) > 0.01f)
            {
                characterInstance.transform.position = Vector3.MoveTowards(characterInstance.transform.position, targetPosition, Time.deltaTime * characterSpeed);
                yield return null;
            }
        }
    }
}