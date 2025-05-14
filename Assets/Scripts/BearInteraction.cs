using UnityEngine;

public class BearInteraction : MonoBehaviour
{
    public GameObject uiPanel;
    private bool isInTrigger = false;

    void Update()
    {
        if (isInTrigger)
            Debug.Log("� ��������");

        if (isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("������ ������� E");
            uiPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerZone"))
        {
            Debug.Log("����� � �������");
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TriggerZone"))
        {
            Debug.Log("����� �� ��������");
            isInTrigger = false;
        }
    }

    public void ClosePanel()
    {
        Debug.Log("�������� ������");
        uiPanel.SetActive(false);
    }
}
