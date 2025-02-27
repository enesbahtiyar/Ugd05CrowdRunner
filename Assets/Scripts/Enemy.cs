using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private EnemyStates state;
    private Transform targetRunner;


    private void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        //Burada enum'ımızın bulunduğu duruma göre neler yapacağını ayarlıyoruz
        switch(state)
        {
            case EnemyStates.Idle:
                SearchForTarget();
                break;
            case EnemyStates.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for(int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                    continue;


                runner.SetTarget();
                targetRunner = runner.transform;

                StartRunningTowardsTarget();
                break;
            }
        }
    }

    private void StartRunningTowardsTarget()
    {
        state = EnemyStates.Running;
        GetComponent<Animator>().Play("Run");

    }
    
    private void RunTowardsTarget()
    {
        if (targetRunner == null)
        {
            SearchForTarget();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetRunner.transform.position, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, targetRunner.transform.position) < .2f)
            {
                Destroy(targetRunner.gameObject);
                Destroy(gameObject);
            }
        }

    }


}
