
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text itemQuantity;
    public TMP_Text itemPrice;

    public Button shopBuyButton;

    public bool isInShop;
    
    public void UpdateButton()
    {
        if (isInShop)
        {
            shopBuyButton.onClick.RemoveAllListeners();
            shopBuyButton.onClick.AddListener(delegate { Shop.buyingItem?.Invoke(this); });
        }
        else
        {
            shopBuyButton.onClick.RemoveAllListeners();
            shopBuyButton.onClick.AddListener(delegate { Inventory.sellingItem?.Invoke(this); });
        }
    }

}
