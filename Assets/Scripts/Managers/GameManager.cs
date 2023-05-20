using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    public PlayerStats playerStats;
    
    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to handle game initialization
    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        // Add your game initialization logic here
        Debug.Log("Game Initialized");
    }

    public int GetPlayerCurrency()
    {
        var playerStatsCurrency = playerStats.currency;
        return playerStatsCurrency;
    }
}
