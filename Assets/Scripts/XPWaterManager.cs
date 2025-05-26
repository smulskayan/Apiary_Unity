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

    public void UpdateXPBar()
    {
        if (xpFillImage != null)
        {
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
