using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;

    [Header("Elements")]
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] Slider progressBar;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] private GameObject shopPanel;


    private void Start()
    {
        progressBar.value = 0;

        gamePanel.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        HideShop();

        levelText.text = "Level " + (ChunkManager.Instance.GetLevel() + 1);
    }

    private void OnEnable()
    {
        GameManager.onGameStateChanged += OnGameStateChangedCallback;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= OnGameStateChangedCallback;
    }

    private void OnGameStateChangedCallback(GameState state)
    {
        if(state == GameState.GameOver)
        {
            ShowGameOver();
        }
        else if(state == GameState.LevelComplete)
        {
            ShowLevelComplete();
        }
    }

    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevelButtonPressed()
    {      
        SceneManager.LoadScene(0);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void ShowLevelComplete()
    {
        levelCompletePanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.instance.isGameState())
            return;

        float progress = PlayerController.Instance.transform.position.z / ChunkManager.Instance.GetFinishZ();

        progressBar.value = progress;
    }

    public void ShowShop()
    {
        shopPanel.SetActive(true);
        shopManager.UpdatePurchaseButton();
    }

    public void HideShop()
    {
        shopPanel.SetActive(false);
    }
}
