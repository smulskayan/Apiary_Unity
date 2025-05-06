using UnityEngine;

public class IconAnimator : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowIcons()
    {
        animator.SetTrigger("SlideIn");
    }

    public void HideIcons()
    {
        animator.SetTrigger("SlideOut");
    }
}