using System;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float goldenAngle = 137.508f;
    [SerializeField] float radius = 137.508f;

    [Header("Elements")]
    [SerializeField] GameObject runnerParent;
    [SerializeField] GameObject runnerPrefab;

    private void Update()
    {
        if (!GameManager.instance.isGameState())
            return;

        PlaceRunners();

        if(runnerParent.transform.childCount <= 0)
        {
            GameManager.instance.SetGameState(GameState.GameOver);
        }
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
    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnerParent.transform.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch(bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;
            case BonusType.Multiplication:
                int runnersToAdd = (runnerParent.transform.childCount * bonusAmount) - runnerParent.transform.childCount;
                AddRunners(runnersToAdd);
                break;
            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                break;
            case BonusType.Division:
                int runnersToRemove = runnerParent.transform.childCount - (runnerParent.transform.childCount / bonusAmount);
                RemoveRunners(runnersToRemove);
                break;
        }
    }


    private void AddRunners(int bonusAmount)
    {
        for (int i = 0; i < bonusAmount; i++)
        {
            GameObject runner = Instantiate(runnerPrefab, runnerParent.transform);
            Animator animator = runner.GetComponent<Animator>();
            animator.Play("Run");
        }        
    }

    private void RemoveRunners(int amount)
    {
        if(amount > runnerParent.transform.childCount)
            amount = runnerParent.transform.childCount;

        int runnerAmount = runnerParent.transform.childCount;

        for (int i = runnerAmount - 1; i >= runnerAmount - amount; i--)
        {
            Transform runnerToDestroy = runnerParent.transform.GetChild(i);
            runnerToDestroy.SetParent(null);

            Destroy(runnerToDestroy.gameObject);
        }
    }
}
