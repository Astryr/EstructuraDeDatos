using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    public TextMeshProUGUI infoText;

    public void Setup(Item item, int quantity)
    {
        infoText.text = $"{item.Name} x{quantity}";
    }
}
