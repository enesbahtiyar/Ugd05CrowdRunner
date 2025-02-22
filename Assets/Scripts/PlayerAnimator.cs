using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Transform runnerParent;

    public void Run()
    {
        for(int i = 0; i < runnerParent.childCount; i++)
        {
            Transform runner = runnerParent.GetChild(i);
            Animator runnerAnim = runner.GetComponent<Animator>();
            runnerAnim.Play("Run");
        }
    }

    public void Idle()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Transform runner = runnerParent.GetChild(i);
            Animator runnerAnim = runner.GetComponent<Animator>();
            runnerAnim.Play("Idle");
        }
    }
}
