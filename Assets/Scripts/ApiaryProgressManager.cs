using UnityEngine;
using UnityEngine.UI;

public class ApiaryProgressManager : MonoBehaviour
{
    public GameObject completedUI;
    public Text coinText;

    void Start()
    {
        int completed = PlayerPrefs.GetInt("MiniGame_Completed", 0);
        int coins = PlayerPrefs.GetInt("MiniGame_Coins", 0);

        if (completed == 1)
        {
            if (completedUI != null)
                completedUI.SetActive(true);

            if (coinText != null)
                coinText.text = "������: " + coins.ToString();
        }
        else
        {
            if (completedUI != null)
                completedUI.SetActive(false);
        }
    }
}
