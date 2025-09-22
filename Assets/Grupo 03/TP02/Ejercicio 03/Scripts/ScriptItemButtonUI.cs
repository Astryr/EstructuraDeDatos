using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class StoreItemButtonUI : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    private Item itemData;
    private StoreManager store;

    public void Setup(Item item, StoreManager manager)
    {
        itemData = item;
        store = manager;

        if (icon != null) icon.sprite = item.Icon;
        if (nameText != null) nameText.text = item.Name;
        if (priceText != null) priceText.text = $"${item.Price}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            store.BuyItem(itemData);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            store.SellItem(itemData);
        }
    }
}


