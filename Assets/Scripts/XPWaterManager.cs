using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class XPWaterManager : MonoBehaviour
{
    public int currentXP = 10;
    public int maxXP = 10;
    public Image xpFillImage;

    void Start()
    {
        xpFillImage.gameObject.SetActive(true);
        xpFillImage.fillAmount = (float)currentXP / maxXP;
    }


    public void GainXP(int amount)
    {
        currentXP += amount;
        currentXP = Mathf.Min(currentXP, maxXP);
        UpdateXPBar();
    }

    public void LoseXP(int amount)
    {
        Debug.Log("Amo" + amount);
        Debug.Log("before currentXP" + currentXP);
        currentXP -= maxXP/amount;
        currentXP = Mathf.Max(currentXP, 0);
        Debug.Log("after currentXP" + currentXP);
        UpdateXPBar();
    }


    public void UpdateXPBar()
    {
        if (xpFillImage != null)
        {
            Debug.Log("live" + Player.flower_count_live);
            Debug.Log("all" + Player.flower_count);
            currentXP = Player.flower_count_live;
            maxXP = Player.flower_count;
            if (currentXP > 0)
            {
                xpFillImage.gameObject.SetActive(true);
                xpFillImage.fillAmount = (float)currentXP / maxXP;
            }
            else
            {
                xpFillImage.gameObject.SetActive(false);
            }
        }
    }
}
