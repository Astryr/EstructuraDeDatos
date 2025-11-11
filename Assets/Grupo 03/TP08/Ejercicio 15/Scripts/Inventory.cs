using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public Transform slotContainer;
    private TextMeshProUGUI[] slotTexts;

   
    public MySet<InventoryItem> items;
    

    private void Awake()
    {
        slotTexts = slotContainer.GetComponentsInChildren<TextMeshProUGUI>();

        
        items = new MySetList<InventoryItem>();
        
    }

    
    public void InitializeInventory(List<InventoryItem> allItems)
    
    {
        items.Clear();

        foreach (var text in slotTexts)
            text.text = "";

        int slotsToFill = 0;
        for (int i = 0; i < slotTexts.Length; i++)
        {
            if (Random.value <= 0.7f)
            {
                
                InventoryItem randomItem = allItems[Random.Range(0, allItems.Count)];
                

                items.Add(randomItem);

                slotTexts[slotsToFill].text = randomItem.name;
                slotsToFill++;
            }
        }
    }
}