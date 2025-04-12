using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private CrowdSystem crowdSystem;
    private void Update()
    {
        if(GameManager.instance.isGameState())
        {
            DetectColliders();
        }

    }

    private void DetectColliders()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, crowdSystem.GetCrowdRadius());

        for(int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Doors doors))
            {
                doors.gameObject.GetComponent<Collider>().enabled = false;
                Debug.Log("Kapıyla Çarpıştık");

                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                crowdSystem.ApplyBonus(bonusType, bonusAmount);
            }

            if (detectedColliders[i].CompareTag("Finish"))
            {
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

                GameManager.instance.SetGameState(GameState.LevelComplete);
            }

            if (detectedColliders[i].CompareTag("Coin"))
            {
                Destroy(detectedColliders[i].gameObject);
                DataManager.Instance.AddCoins(1);
            }
        }
    }
}
