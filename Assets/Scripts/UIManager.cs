using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] Slider progressBar;
    [SerializeField] TextMeshProUGUI levelText;


    private void Start()
    {
        progressBar.value = 0;

        gamePanel.SetActive(true);
        menuPanel.SetActive(true);

        levelText.text = "Level " + (ChunkManager.instance.GetLevel() + 1);
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

        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishZ();

        progressBar.value = progress;
    }
}
