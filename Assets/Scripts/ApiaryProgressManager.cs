using UnityEngine;
using UnityEngine.UI;

public class ApiaryProgressManager : MonoBehaviour
{
    public GameObject completedUI; // Панель или иконка "мини-игра пройдена"
    public Text coinText; // UI текст с количеством монет

    void Start()
    {
        int completed = PlayerPrefs.GetInt("MiniGame_Completed", 0);
        int coins = PlayerPrefs.GetInt("MiniGame_Coins", 0);

        if (completed == 1)
        {
            Debug.Log("Мини-игра пройдена!");
            if (completedUI != null)
                completedUI.SetActive(true);

            if (coinText != null)
                coinText.text = "Монеты: " + coins.ToString();
        }
        else
        {
            if (completedUI != null)
                completedUI.SetActive(false);
        }
    }
}
