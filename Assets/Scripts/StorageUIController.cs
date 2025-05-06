using UnityEngine;

public class StorageUIController : MonoBehaviour
{
    public GameObject storagePanel; // Панель UI
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            TogglePanel();
        }
    }

    void TogglePanel()
    {
        storagePanel.SetActive(!storagePanel.activeSelf);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            storagePanel.SetActive(false); // Закрываем, если игрок ушёл
        }
    }
}
