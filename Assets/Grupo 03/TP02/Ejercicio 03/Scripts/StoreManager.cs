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
        AddItemToStore(new Item { ID = 2, Name = "Poción de Salud", Price = 50, Rarity = "Común", Type = "Consumible" });
        AddItemToStore(new Item { ID = 3, Name = "Armadura", Price = 200, Rarity = "Rara", Type = "Armadura" });
        AddItemToStore(new Item { ID = 4, Name = "Escudo", Price = 300, Rarity = "Rara", Type = "Armadura" });
        AddItemToStore(new Item { ID = 5, Name = "Daga", Price = 120, Rarity = "Común", Type = "Arma" });
        AddItemToStore(new Item { ID = 6, Name = "Casco", Price = 100, Rarity = "Rara", Type = "Armadura" });
        AddItemToStore(new Item { ID = 7, Name = "Botas", Price = 50, Rarity = "Común", Type = "Armadura" });
        AddItemToStore(new Item { ID = 8, Name = "Lanza", Price = 300, Rarity = "Rara", Type = "Arma" });
        AddItemToStore(new Item { ID = 9, Name = "Varita", Price = 150, Rarity = "Rara", Type = "Arma" });
        AddItemToStore(new Item { ID = 10, Name = "Poción de Veneno", Price = 70, Rarity = "Común", Type = "Consumible" });

        GenerateUI();
    }

    void AddItemToStore(Item item)
    {
        item.Price = LoadItemPrice(item.ID, item.Price);
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
            item.Price = Mathf.RoundToInt(item.Price * 1.10f); // Aumenta 10%
            SaveItemPrice(item); // Guardar en PlayerPrefs
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
        item.Price = Mathf.RoundToInt(item.Price * 0.90f); // Reduce 10%
        SaveItemPrice(item); // Guardar en PlayerPrefs
    }
    void SaveItemPrice(Item item)
    {
        PlayerPrefs.SetInt("ItemPrice_" + item.ID, item.Price);
        PlayerPrefs.Save();
    }

    int LoadItemPrice(int itemID, int defaultPrice)
    {
        return PlayerPrefs.GetInt("ItemPrice_" + itemID, defaultPrice);
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


