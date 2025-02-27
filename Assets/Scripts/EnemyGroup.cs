using System;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform enemyParent;

    [Header("Settings")]
    [SerializeField] private int amount;
    [SerializeField] float goldenAngle = 137.508f;
    [SerializeField] float radius = 1f;

    private void Start()
    {
        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 enemyLocalPosition = GetRunnerLocalPosition(i);

            Vector3 enemyWorldPosition = transform.TransformPoint(enemyLocalPosition);

            Instantiate(enemyPrefab, enemyWorldPosition, Quaternion.identity, enemyParent);          
        }
    }


    private Vector3 GetRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * goldenAngle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * goldenAngle);

        return new Vector3(x, 0, z);
    }
}
