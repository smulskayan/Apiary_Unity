using UnityEngine;

public class StorageInteraction : MonoBehaviour
{
    public GameObject player;
    public GameObject storagePanel;
    public float interactionRange = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
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
        }
    }

    public void CloseStoragePanel()
    {
        StartCoroutine(DelayedClose());
    }

    private System.Collections.IEnumerator DelayedClose()
    {
        yield return new WaitForEndOfFrame();
        storagePanel.SetActive(false);
    }
}
