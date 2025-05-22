using UnityEngine;
using UnityEngine.UI; // ���� ����������� ����������� UI
using TMPro; // ���� ����������� TextMeshPro

public class StorageUI : MonoBehaviour
{
    public Storage storage; // ������ �� Storage.cs
    public GameObject honeyDisplay; // ������ � ������� � ������
    public TextMeshProUGUI honeyAmountText; // ��������� ���� ��� ���������� ���

    void OnEnable()
    {
        UpdateHoneyUI();
    }

    public void UpdateHoneyUI()
    {
        int honeyAmount = storage.GetHoneyAmount();
        honeyDisplay.SetActive(honeyAmount > 0);
        honeyAmountText.text = honeyAmount.ToString();
    }
}
