using UnityEngine;

public class BearInteraction : MonoBehaviour
{
    public GameObject uiPanel;
    private bool isInTrigger = false;

    void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            uiPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerZone"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TriggerZone"))
        {
            isInTrigger = false;
        }
    }

    public void ClosePanel()
    {
        uiPanel.SetActive(false);
    }
}
