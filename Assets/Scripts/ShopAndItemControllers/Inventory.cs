using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private GameObject _itemContainer;
    private GameObject _toInstantiate;
    public GameObject itemPrefab;
    public RectTransform inventoryScrollRect;
    private Item _item;
    public static Action<Item> sellingItem;
    private PlayerStats _playerStats;

    public Shop shopRef;

    private void Awake()
    {
        _item = itemPrefab.GetComponent<Item>();
    }
    
    private void OnEnable()
    {
        sellingItem += SellItem;
    }

    private void OnDisable()
    {
        sellingItem -= SellItem;
    }

    private void Start()
    {
        _playerStats = GameManager.instance.playerStats;

        foreach (Item item in _playerStats.inventory)
        {
            Item newItem = Instantiate(item.gameObject, inventoryScrollRect).GetComponent<Item>();
            newItem.isInShop = false;
            newItem.UpdateButton();
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(inventoryScrollRect);
        Destroy(_itemContainer);
    }

    
    private void SellItem(Item item)
    {
        if (shopRef.gameObject.activeSelf)
        {
            item.itemQuantity.text = (int.Parse(item.itemQuantity.text) - 1).ToString();
            _playerStats.AddCurrency(int.Parse(item.itemPrice.text));

            if (int.Parse(item.itemQuantity.text) <= 0)
            {
                Destroy(item.gameObject);
                LayoutRebuilder.ForceRebuildLayoutImmediate(inventoryScrollRect);
            }

            // Create a new Item object in the shop with a quantity of 1
            Shop shop = FindObjectOfType<Shop>();
            Item newItem = Instantiate(shop.itemPrefab, shop.ShopScrollRect).GetComponent<Item>();
            newItem.itemName.text = item.itemName.text;
            newItem.itemPrice.text = item.itemPrice.text;
            newItem.itemQuantity.text = "1";
            newItem.isInShop = true;
            newItem.UpdateButton();

            GameManager.instance.playerStats.inventory.Remove(item);

            LayoutRebuilder.ForceRebuildLayoutImmediate(inventoryScrollRect);
            Debug.Log($"Selling item: {item.itemName.text}.");   
        }
    }

}
