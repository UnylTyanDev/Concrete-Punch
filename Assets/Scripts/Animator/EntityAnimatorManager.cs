using UnityEngine;

public class EntityAnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;


    public void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }
}
