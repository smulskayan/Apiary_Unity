using UnityEngine;

public class StorageInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject storagePanel;
    public float interactionRange = 2f;
    public AudioClip openSound; // Звук открытия склада
    public AudioClip closeSound; // Звук закрытия склада
    private AudioSource audioSource; // Ссылка на AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Получаем AudioSource
    }

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

        float distance = Vector3.Distance(player.transform.position, storage.transform.position);
        if (distance <= interactionRange)
        {
            storagePanel.SetActive(true);
            if (audioSource != null && openSound != null)
            {
                audioSource.PlayOneShot(openSound); // Воспроизводим звук открытия
            }
        }
    }

    public void CloseStoragePanel()
    {
        StartCoroutine(DelayedClose());
    }

    private System.Collections.IEnumerator DelayedClose()
    {
        if (audioSource != null && closeSound != null)
        {
            audioSource.PlayOneShot(closeSound); // Воспроизводим звук закрытия
        }
        yield return new WaitForEndOfFrame();
        storagePanel.SetActive(false);
    }
}