using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string name;
    public int price;

    public InventoryItem(string name, int price)
    {
        this.name = name;
        this.price = price;
    }

    public override bool Equals(object obj)
    {
        if (obj is InventoryItem other)
            return name == other.name;
        return false;
    }

    public override int GetHashCode()
    {
        return name.GetHashCode();
    }

    public override string ToString()
    {
        return $"{name} (${price})";
    }
}