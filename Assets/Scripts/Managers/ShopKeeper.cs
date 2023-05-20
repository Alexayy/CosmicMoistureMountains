using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public BoxCollider2D _collider2D;
    public UIManager uiManager;
    public bool playerInRange = false;
    
    private void Start()
    {
        _collider2D.isTrigger = true;
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            // Open or close the shop UI
            uiManager.ToggleShop(!uiManager._shop.activeSelf);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // display a prompt to let the player know they can press F to open the shop
            uiManager.ShowPrompt("Press F to open shop");
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.HidePrompt();
            uiManager.ToggleShop(false);
            playerInRange = false;
        }
    }
}
