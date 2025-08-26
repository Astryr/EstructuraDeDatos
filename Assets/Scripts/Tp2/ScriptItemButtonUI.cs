using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class StoreItemButtonUI : MonoBehaviour
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
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        store.BuyItem(itemData);
    }
}

