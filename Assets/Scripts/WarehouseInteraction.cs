using UnityEngine;

public class WarehouseInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject interactButton;
    public GameObject minigamePanel;
    public float interactionRange = 2f;
    private HoneyCatchMinigame minigame;

    void Start()
    {
        interactButton.SetActive(false);
        minigame = FindObjectOfType<HoneyCatchMinigame>();
    }

    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance <= interactionRange)
        {
            interactButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                minigame.StartMinigame();
            }
        }
        else
        {
            interactButton.SetActive(false);
        }
    }
}