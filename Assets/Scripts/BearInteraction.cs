using UnityEngine;

public class BearInteraction : MonoBehaviour
{
    public GameObject uiPanel;
    private bool isInTrigger = false;

    void Update()
    {
        if (isInTrigger)
            Debug.Log("В триггере");

        if (isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Нажата клавиша E");
            uiPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerZone"))
        {
            Debug.Log("Вошёл в триггер");
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TriggerZone"))
        {
            Debug.Log("Вышел из триггера");
            isInTrigger = false;
        }
    }

    public void ClosePanel()
    {
        Debug.Log("Закрытие панели");
        uiPanel.SetActive(false);
    }
}
