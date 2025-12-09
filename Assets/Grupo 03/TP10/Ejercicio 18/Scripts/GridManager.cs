using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI solutionText;

    [Header("Configuración del Personaje")]
    public GameObject characterPrefab;
    public float characterSpeed = 5f; 

    private Tile[,] grid;
    private TileType currentTileType = TileType.Floor;
    private Node entryPoint = null;
    private Node exitPoint = null;
    private Pathfinder pathfinder;
    private List<Node> currentPath;
    private GameObject characterInstance;
    private bool isMoving = false;

    void Start()
    {
        pathfinder = new Pathfinder();
        GenerateGrid();
        Camera.main.transform.position = new Vector3((float)gridWidth / 2 - 0.5f, (float)gridHeight / 2 - 0.5f, -10);
        Camera.main.orthographicSize = Mathf.Max(gridWidth, gridHeight) / 2f + 1;
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
                tile.Initialize(x, y, TileType.Floor);
                tile.SetType(TileType.Floor, floorColor);
                grid[x, y] = tile;
            }
        }
    }

    void HandlePaintingInput()
    {
        
        if (Input.GetMouseButton(0) && !isMoving)
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
                grid[entryPoint.X, entryPoint.Y].SetType(TileType.Floor, floorColor);
            entryPoint = new Node(x, y);
        }
        else if (currentTileType == TileType.Exit)
        {
            if (exitPoint != null)
                grid[exitPoint.X, exitPoint.Y].SetType(TileType.Floor, floorColor);
            exitPoint = new Node(x, y);
        }

        if (tileToPaint.type == TileType.Entrance) entryPoint = null;
        if (tileToPaint.type == TileType.Exit) exitPoint = null;

        tileToPaint.SetType(currentTileType, GetColorForType(currentTileType));
    }

    Color GetColorForType(TileType type)
    {
        switch (type)
        {
            case TileType.Entrance: return entranceColor;
            case TileType.Exit: return exitColor;
            case TileType.Wall: return wallColor;
            case TileType.Floor: default: return floorColor;
        }
    }

    public void SelectEntrance() => currentTileType = TileType.Entrance;
    public void SelectExit() => currentTileType = TileType.Exit;
    public void SelectFloor() => currentTileType = TileType.Floor;
    public void SelectWall() => currentTileType = TileType.Wall;

    public void CheckForSolution()
    {
        if (entryPoint == null || exitPoint == null)
        {
            solutionText.text = "Faltan Entrada (Azul) o Salida (Rojo)";
            return;
        }

        currentPath = pathfinder.FindPath(grid, entryPoint, exitPoint);

        if (currentPath != null && currentPath.Count > 0)
        {
            solutionText.text = $"¡Camino encontrado! Pasos: {currentPath.Count}";
        }
        else
        {
            solutionText.text = "No hay solución posible.";
            currentPath = null;
        }
    }

    public void StartCharacterMovement()
    {
        if (currentPath != null && currentPath.Count > 0 && !isMoving)
        {
            if (characterInstance != null) Destroy(characterInstance);

            characterInstance = Instantiate(characterPrefab, new Vector3(entryPoint.X, entryPoint.Y, -1), Quaternion.identity);
            StartCoroutine(MoveCharacterCoroutine());
        }
        else if (currentPath == null)
        {
            solutionText.text = "Primero verifica si hay solución.";
        }
    }

    IEnumerator MoveCharacterCoroutine()
    {
        isMoving = true;
        foreach (Node node in currentPath)
        {
            Vector3 targetPosition = new Vector3(node.X, node.Y, -1); 

            
            while (Vector3.Distance(characterInstance.transform.position, targetPosition) > 0.01f)
            {
                characterInstance.transform.position = Vector3.MoveTowards(characterInstance.transform.position, targetPosition, Time.deltaTime * characterSpeed);
                yield return null;
            }
            characterInstance.transform.position = targetPosition; 
        }
        isMoving = false;
        solutionText.text = "¡Llegada!";
    }
}