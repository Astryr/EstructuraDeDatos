using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory player1;
    public Inventory player2;
    public TMP_Text resultText;
    public Button regenerateButton;

    
    private List<InventoryItem> allItems;
    private MySet<InventoryItem> allItemsSet;


    void Start()
    {
        allItems = new List<InventoryItem>();
        allItemsSet = new MySetList<InventoryItem>();
        for (int i = 0; i < 40; i++)
        {
           
            int randomPrice = Random.Range(10, 201);
            InventoryItem newItem = new InventoryItem($"Item {i + 1}", randomPrice);
            

            allItems.Add(newItem);
            allItemsSet.Add(newItem);
        }

        Regenerate();

        if (regenerateButton != null)
            regenerateButton.onClick.AddListener(Regenerate);
    }

    public void ShowUniqueItems()
    {
       
        MySet<InventoryItem> uniqueToP1 = player1.items.Difference(player2.items);
        MySet<InventoryItem> uniqueToP2 = player2.items.Difference(player1.items);

        string result = "Ítems únicos de Jugador 1:\n";
        if (uniqueToP1.Elements.Length > 0)
            result += string.Join("\n", uniqueToP1.Elements.Select(i => i.ToString()));
        else
            result += "(Ninguno)\n";

        result += "\n\nÍtems únicos de Jugador 2:\n";
        if (uniqueToP2.Elements.Length > 0)
            result += string.Join("\n", uniqueToP2.Elements.Select(i => i.ToString()));
        else
            result += "(Ninguno)";
       

        resultText.text = result;
    }


    public void ShowMissingItems()
    {
        
        MySet<InventoryItem> owned = player1.items.Union(player2.items);
        MySet<InventoryItem> missing = allItemsSet.Difference(owned);
        ShowResult("Ítems que ninguno tiene:", missing.Elements.ToList());
        
    }

    public void ShowCounts()
    {
        resultText.text = $"Jugador 1: {player1.items.Cardinality()} ítems\n" +
                          $"Jugador 2: {player2.items.Cardinality()} ítems";
    }

   
    private void ShowResult(string title, List<InventoryItem> items)
    
    {
        if (items.Count == 0)
        {
            resultText.text = $"{title}\n(Ninguno)";
            return;
        }

        resultText.text = title + "\n" + string.Join("\n", items.Select(i => i.ToString()));
    }

    public void Regenerate()
    {
        player1.InitializeInventory(allItems);
        player2.InitializeInventory(allItems);
        resultText.text = "Inventarios generados nuevamente.\nPresiona un botón para comparar.";
    }
}