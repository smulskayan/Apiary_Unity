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
    public AudioClip levelUpSound; // ���� ��������� ������
    private AudioSource audioSource; // ������ �� AudioSource
    public GameObject levelUpPanel; // ������ ��� ������������ ����
    public TextMeshProUGUI levelUpPanelText; // ����� � ����������� ����
    public float popupDuration = 2f; // ������������ ����������� ����

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // �������� AudioSource
        UpdateXPBar();
        UpdateLevelText();
        if (levelUpPanel != null)
        {
            levelUpPanel.SetActive(false); // �������� ������ ��� ������
        }
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

        // ��������������� ����� ��������� ������
        if (audioSource != null && levelUpSound != null)
        {
            audioSource.PlayOneShot(levelUpSound);
        }

        // ����� ������������ ����
        if (levelUpPanel != null && levelUpPanelText != null)
        {
            levelUpPanelText.text = $"{level}";
            StartCoroutine(ShowLevelUpPopup());
        }
    }

    private System.Collections.IEnumerator ShowLevelUpPopup()
    {
        levelUpPanel.SetActive(true);

        // �������� ��������� (���������������)
        CanvasGroup canvasGroup = levelUpPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = levelUpPanel.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        float timer = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 targetScale = Vector3.one;

        // ��������� � ���������������� � ����������
        while (timer < popupDuration / 2)
        {
            timer += Time.deltaTime;
            float t = timer / (popupDuration / 2);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            levelUpPanel.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        levelUpPanel.transform.localScale = targetScale;

        // �������� ����� �������������
        yield return new WaitForSeconds(popupDuration / 2);

        // �������� ������������
        timer = 0f;
        while (timer < popupDuration / 2)
        {
            timer += Time.deltaTime;
            float t = timer / (popupDuration / 2);
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }

        levelUpPanel.SetActive(false);
    }
}