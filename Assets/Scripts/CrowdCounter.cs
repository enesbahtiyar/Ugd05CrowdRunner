using UnityEngine;
using TMPro;

public class CrowdCounter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshPro crowdCounterText;
    [SerializeField] private Transform runnerParent;

    private void Update()
    {
        crowdCounterText.text = runnerParent.childCount.ToString();

        if(runnerParent.childCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
