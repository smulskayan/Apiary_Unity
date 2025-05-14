using UnityEngine;
using System.Collections;

public class StorageInteraction : MonoBehaviour
{
    public GameObject player;          // �����
    public GameObject storagePanel;    // UI ������
    public float interactionRange = 2f; // ������ ��������������

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
            Debug.LogWarning("���-�� �� ���������!");
            return;
        }

        float distance = Vector3.Distance(player.transform.position, storage.transform.position);

        if (distance <= interactionRange)
        {
            Debug.Log("��������� �����");
            storagePanel.SetActive(true);
        }
    }

    public void CloseStoragePanel()
    {
        Debug.Log("��������� ����� � ����� ��������");
        StartCoroutine(DelayedClose());
    }

    private IEnumerator DelayedClose()
    {
        yield return new WaitForEndOfFrame(); // ��������� ���� ����
        storagePanel.SetActive(false);
    }
}
