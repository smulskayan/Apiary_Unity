using UnityEngine;

public class StorageInteraction : MonoBehaviour
{
    public GameObject player;               // Игрок
    public GameObject storagePanel;         // UI-панель склада
    public float interactionRange = 2f;     // Радиус взаимодействия

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed in StorageInteraction");
            TryOpenStorage();
        }
    }

    void TryOpenStorage()
    {
        GameObject storage = GameObject.FindGameObjectWithTag("Storage");

        if (storage == null)
        {
            Debug.LogWarning("No GameObject with tag 'Storage' found!");
            return;
        }
        if (player == null)
        {
            Debug.LogWarning("Player not assigned in StorageInteraction!");
            return;
        }
        if (storagePanel == null)
        {
            Debug.LogWarning("StoragePanel not assigned in StorageInteraction!");
            return;
        }

        float distance = Vector3.Distance(player.transform.position, storage.transform.position);
        Debug.Log($"Distance to storage: {distance}, interactionRange: {interactionRange}");

        if (distance <= interactionRange)
        {
            Debug.Log("Opening storage panel");
            storagePanel.SetActive(true);

            // Обновляем UI при открытии
            StorageUI storageUI = storagePanel.GetComponent<StorageUI>();
            if (storageUI != null)
            {
                storageUI.UpdateHoneyUI();
            }
            else
            {
                Debug.LogWarning("StorageUI component not found on storagePanel.");
            }
        }
        else
        {
            Debug.Log($"Player too far from storage: {distance} > {interactionRange}");
        }
    }

    public void CloseStoragePanel()
    {
        Debug.Log("Closing storage panel — with delay");
        StartCoroutine(DelayedClose());
    }

    private System.Collections.IEnumerator DelayedClose()
    {
        yield return new WaitForEndOfFrame();
        storagePanel.SetActive(false);
    }
}
