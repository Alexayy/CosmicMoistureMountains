using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int currency;
    public List<Item> inventory;

    void Start()
    {
        // Initialize currency
        currency = 1000;
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
    }

    public void DeductCurrency(int amount)
    {
        currency -= amount;
    }
}
