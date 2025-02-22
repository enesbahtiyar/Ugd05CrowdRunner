using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private CrowdSystem crowdSystem;
    private void Update()
    {
        DetectDoors();
    }

    private void DetectDoors()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1);

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
                Debug.Log("Oyun bitti");

                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

                SceneManager.LoadScene(0);
            }
        }
    }
}
