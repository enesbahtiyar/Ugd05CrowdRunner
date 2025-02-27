using UnityEngine;

public class Runner : MonoBehaviour
{
    private bool isTarget;

    public void SetTarget()
    {
        isTarget = true;
    }

    public bool IsTarget()
    {
        return isTarget;
    }
}
