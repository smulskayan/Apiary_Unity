using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    public int currentXP = 0;
    public int maxXP = 10;
    public int level = 1;

    public Image xpFillImage;
    public TextMeshProUGUI levelText;
    public GameObject levelUpEffect;

    void Start()
    {
        UpdateXPBar();
        UpdateLevelText();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= maxXP)
        {
            LevelUp();
        }

        currentXP = Mathf.Min(currentXP, maxXP);
        UpdateXPBar();
    }

    private void UpdateXPBar()
    {
        if (xpFillImage != null)
        {
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

    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = level.ToString();
        }
    }

    private void LevelUp()
    {
        level++;
        currentXP = 0;

        UpdateXPBar();
        UpdateLevelText();

        if (levelUpEffect != null)
        {
            Instantiate(levelUpEffect, transform.position, Quaternion.identity);
        }
    }
}
