using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float goldenAngle = 137.508f;
    [SerializeField] float radius = 137.508f;

    [SerializeField] GameObject runnerParent;

    private void Update()
    {
        PlaceRunners();
    }

    void PlaceRunners()
    {
        for (int i = 0; i < runnerParent.transform.childCount; i++)
        {
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnerParent.transform.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * goldenAngle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * goldenAngle);

        return new Vector3(x, 0, z);
    }
}
