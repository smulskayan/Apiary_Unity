using UnityEngine;
using UnityEngine.UI; // Если используешь стандартный UI
using TMPro; // Если используешь TextMeshPro

public class StorageUI : MonoBehaviour
{
    public Storage storage; // Ссылка на Storage.cs
    public GameObject honeyDisplay; // Панель с иконкой и числом
    public TextMeshProUGUI honeyAmountText; // Текстовое поле для количества мёда

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
