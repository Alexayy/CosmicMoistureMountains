using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject _shop;
    public GameObject _inventory;
    public TMP_Text _currency;
    public TMP_Text _prompt;
    
    private PlayerStats _playerStats;
    
    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _playerStats = GameManager.instance.playerStats;
    }

    private void Update()
    {
        _currency.text = _playerStats.currency.ToString();
    }

    public void ToggleShop(bool toggle)
    { 
        _shop.SetActive(toggle);
    }
    
    public void ToggleInventory(bool toggle)
    { 
        _inventory.SetActive(toggle);
    }
    
    public void ShowPrompt(string text)
    {
        _prompt.text = text;
        _prompt.gameObject.SetActive(true);
    }

    public void HidePrompt()
    {
        _prompt.gameObject.SetActive(false);
    }
}
