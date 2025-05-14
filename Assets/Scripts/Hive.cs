using UnityEngine;

public class Hive : MonoBehaviour
{
    public IconAnimator iconAnimator; // Ссылка на IconAnimator

    void Start()
    {
        // Найти IconAnimator, если не назначен
        if (iconAnimator == null)
        {
            iconAnimator = GetComponentInChildren<IconAnimator>();
        }
        if (iconAnimator == null)
        {
            Debug.LogWarning("IconAnimator not found in " + gameObject.name);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (iconAnimator != null)
            {
                iconAnimator.ShowIcons();
            }
            Debug.Log("Player entered hive trigger, showing icons");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (iconAnimator != null)
            {
                iconAnimator.HideIcons();
            }
            Debug.Log("Player exited hive trigger, hiding icons");
        }
    }
}