using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }







   
    private GameState gameState;

    //Event
    //static bir event olu?turmam?z gerekiyor
    public static Action<GameState> onGameStateChanged;

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;

        onGameStateChanged?.Invoke(gameState);

        Debug.Log("Game State Changed: " + gameState);
    }

    public bool isGameState()
    {
        return gameState == GameState.Game;
    }
}
