using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public int money = 200;
    public Dictionary<int, int> items = new Dictionary<int, int>();
    public TextMeshProUGUI moneyText;

    public GameObject inventoryItemPrefab;
    public Transform inventoryPanel;

    void Update()
    {
        if (moneyText != null)
            moneyText.text = $"Dinero: ${money}";
    }

    public void AddItem(Item item)
    {
        if (items.ContainsKey(item.ID))
            items[item.ID]++;
        else
            items[item.ID] = 1;

        UpdateInventoryUI(Object.FindFirstObjectByType<StoreManager>().storeItems);
    }

    public void SellItem(Item item)
    {
        if (items.ContainsKey(item.ID) && items[item.ID] > 0)
        {
            items[item.ID]--;
            money += item.Price;
            Debug.Log($"Vendiste: {item.Name}");
            UpdateInventoryUI(Object.FindFirstObjectByType<StoreManager>().storeItems);
        }
        else
        {
            Debug.Log("No tienes ese ítem para vender.");
        }
    }


    public void UpdateInventoryUI(Dictionary<int, Item> storeItems)
    {
        foreach (Transform child in inventoryPanel) Destroy(child.gameObject);

        foreach (var kvp in items)
        {
            if (kvp.Value > 0 && storeItems.ContainsKey(kvp.Key))
            {
                GameObject itemObj = Instantiate(inventoryItemPrefab, inventoryPanel);
                InventoryItemUI itemUI = itemObj.GetComponent<InventoryItemUI>();
                itemUI.Setup(storeItems[kvp.Key], kvp.Value);
            }
        }
    }
}



