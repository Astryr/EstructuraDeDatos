using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public int money = 200;
    public Dictionary<int, int> items = new Dictionary<int, int>();
    public TextMeshProUGUI moneyText;

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
    }

    public void SellItem(Item item)
    {
        if (items.ContainsKey(item.ID) && items[item.ID] > 0)
        {
            items[item.ID]--;
            money += item.Price;
            Debug.Log($"Vendiste: {item.Name}");
        }
    }
}

