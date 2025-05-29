using UnityEngine;

public class StorageInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject storagePanel;
    public float interactionRange = 2f;
    public AudioClip openSound; // ���� �������� ������
    public AudioClip closeSound; // ���� �������� ������
    private AudioSource audioSource; // ������ �� AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // �������� AudioSource
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
                audioSource.PlayOneShot(openSound); // ������������� ���� ��������
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
            audioSource.PlayOneShot(closeSound); // ������������� ���� ��������
        }
        yield return new WaitForEndOfFrame();
        storagePanel.SetActive(false);
    }
}