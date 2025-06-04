using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HiveInteraction : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    public GameObject miniGamePanel;
    [SerializeField] private float triggerRadius = 2f; 

    private void Start()
    {
        if (playButton != null)
            playButton.SetActive(false);

        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
            collider.radius = triggerRadius;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playButton != null)
                playButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playButton != null)
                playButton.SetActive(false);
        }
    }

    public void OpenMiniGame()
    {
        miniGamePanel.SetActive(true);
    }
    public void CloseMiniGame()
    {
        miniGamePanel.SetActive(false);

    }
}