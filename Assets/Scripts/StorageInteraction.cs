using UnityEngine;
using System.Collections;

public class StorageInteraction : MonoBehaviour
{
    public GameObject player;          // Игрок
    public GameObject storagePanel;    // UI панель
    public float interactionRange = 2f; // Радиус взаимодействия

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpenStorage();
        }
    }

    void TryOpenStorage()
    {
        GameObject storage = GameObject.FindGameObjectWithTag("Storage");

        if (storage == null || player == null || storagePanel == null)
        {
            Debug.LogWarning("Что-то не назначено!");
            return;
        }

        float distance = Vector3.Distance(player.transform.position, storage.transform.position);

        if (distance <= interactionRange)
        {
            Debug.Log("Открываем склад");
            storagePanel.SetActive(true);
        }
    }

    public void CloseStoragePanel()
    {
        Debug.Log("Закрываем склад — через задержку");
        StartCoroutine(DelayedClose());
    }

    private IEnumerator DelayedClose()
    {
        yield return new WaitForEndOfFrame(); // подождать один кадр
        storagePanel.SetActive(false);
    }
}
