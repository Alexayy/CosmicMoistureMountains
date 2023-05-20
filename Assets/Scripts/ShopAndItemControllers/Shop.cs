using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    private GameObject _itemContainer;
    private GameObject _toInstantiate;
    public GameObject itemPrefab;
    public RectTransform ShopScrollRect;
    public RectTransform inventoryScrollRect;
    private Item _item;

    public static Action<Item> buyingItem;

    private PlayerStats _playerStats;

    private void Awake()
    {
        _item = itemPrefab.GetComponent<Item>();
    }

    private void OnEnable()
    {
        buyingItem += BuyItem;
    }

    private void OnDisable()
    {
        buyingItem -= BuyItem;
    }

    private void Start()
    {
        _playerStats = GameManager.instance.playerStats;

        for (int i = 0; i < 50; i++)
        {
            _item.itemName.text = $"{Random.Range(700, 1000)} Aleksa";
            _item.itemPrice.text = Random.Range(10, 500).ToString();
            _item.itemQuantity.text = Random.Range(1, 9).ToString();

            Item newItem = Instantiate(_item.gameObject, ShopScrollRect).GetComponent<Item>();
            newItem.isInShop = true;
            newItem.UpdateButton();
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(ShopScrollRect);
        Destroy(_itemContainer);
    }


    private void BuyItem(Item item)
    {
        if (_playerStats.currency <= int.Parse(item.itemPrice.text))
            return;

        item.itemQuantity.text = (int.Parse(item.itemQuantity.text) - 1).ToString();
        _playerStats.DeductCurrency(int.Parse(item.itemPrice.text));

        if (int.Parse(item.itemQuantity.text) < 1)
        {
            Destroy(item.gameObject);
            LayoutRebuilder.ForceRebuildLayoutImmediate(ShopScrollRect);
            return;
        }

        // Create a new Item object with a quantity of 1
        Item newItem = Instantiate(itemPrefab, inventoryScrollRect).GetComponent<Item>();
        newItem.itemName.text = item.itemName.text;
        newItem.itemPrice.text = item.itemPrice.text;
        newItem.itemQuantity.text = "1";
        newItem.isInShop = false;
        newItem.UpdateButton();

        GameManager.instance.playerStats.inventory.Add(newItem);

        LayoutRebuilder.ForceRebuildLayoutImmediate(ShopScrollRect);
        Debug.Log($"Buying item: {item.itemName.text}.");
    }


}
