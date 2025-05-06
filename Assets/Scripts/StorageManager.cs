using UnityEngine;
using UnityEngine.UI;

public class StorageManager : MonoBehaviour
{
    public int honey = 0;
    public int seeds = 0;
    public int bees = 0;

    public Text honeyText;
    public Text seedsText;
    public Text beesText;

    void Start()
    {
        UpdateUI();
    }

    public void AddHoney(int amount)
    {
        honey += amount;
        UpdateUI();
    }

    public void AddSeeds(int amount)
    {
        seeds += amount;
        UpdateUI();
    }

    public void AddBees(int amount)
    {
        bees += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        honeyText.text = "Мёд: " + honey;
        seedsText.text = "Семена: " + seeds;
        beesText.text = "Пчёлы: " + bees;
    }
}
