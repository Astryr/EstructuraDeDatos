using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public Dictionary<int, Item> storeItems = new Dictionary<int, Item>();
    public GameObject buttonPrefab;
    public Transform contentPanel;
    public PlayerInventory player;

    void Start()
    {
        AddItemToStore(new Item { ID = 1, Name = "Espada", Price = 100, Rarity = "Común", Type = "Arma" });
        AddItemToStore(new Item { ID = 2, Name = "Poción", Price = 50, Rarity = "Común", Type = "Consumible" });
        AddItemToStore(new Item { ID = 3, Name = "Armadura", Price = 200, Rarity = "Rara", Type = "Armadura" });

        GenerateUI();
    }

    void AddItemToStore(Item item)
    {
        if (!storeItems.ContainsKey(item.ID))
            storeItems.Add(item.ID, item);
    }

    void GenerateUI()
    {
        foreach (Transform child in contentPanel) Destroy(child.gameObject);
        foreach (KeyValuePair<int, Item> kvp in storeItems)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, contentPanel);
            StoreItemButtonUI btnUI = buttonObj.GetComponent<StoreItemButtonUI>();
            btnUI.Setup(kvp.Value, this);
        }
    }

    public void BuyItem(Item item)
    {
        if (player.money >= item.Price)
        {
            player.money -= item.Price;
            player.AddItem(item);
            Debug.Log($"Compraste: {item.Name}");
        }
        else
        {
            Debug.Log("No tienes suficiente dinero.");
        }
    }

    public void SellItem(Item item)
    {
        player.SellItem(item);
    }

    public void SortItems(string criteria)
    {
        List<Item> sortedList = new List<Item>(storeItems.Values);

        switch (criteria)
        {
            case "ID":
                sortedList.Sort((a, b) => a.ID.CompareTo(b.ID));
                break;
            case "Name":
                sortedList.Sort((a, b) => a.Name.CompareTo(b.Name));
                break;
            case "Price":
                sortedList.Sort((a, b) => a.Price.CompareTo(b.Price));
                break;
            case "Rarity":
                sortedList.Sort((a, b) => a.Rarity.CompareTo(b.Rarity));
                break;
            case "Type":
                sortedList.Sort((a, b) => a.Type.CompareTo(b.Type));
                break;
        }

        foreach (Transform child in contentPanel) Destroy(child.gameObject);
        foreach (Item item in sortedList)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, contentPanel);
            StoreItemButtonUI btnUI = buttonObj.GetComponent<StoreItemButtonUI>();
            btnUI.Setup(item, this);
        }
    }
}


