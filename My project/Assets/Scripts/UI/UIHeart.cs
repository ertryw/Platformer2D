using UnityEngine;

public class UIHeart : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void HeartLose()
    {
        animator.SetTrigger("Lose");
    }
}
