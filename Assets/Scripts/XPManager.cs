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
    public AudioClip levelUpSound; // Звук повышения уровня
    private AudioSource audioSource; // Ссылка на AudioSource
    public GameObject levelUpPanel; // Панель для выплывающего окна
    public TextMeshProUGUI levelUpPanelText; // Текст в выплывающем окне
    public float popupDuration = 2f; // Длительность отображения окна

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Получаем AudioSource
        UpdateXPBar();
        UpdateLevelText();
        if (levelUpPanel != null)
        {
            levelUpPanel.SetActive(false); // Скрываем панель при старте
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

        // Воспроизведение звука повышения уровня
        if (audioSource != null && levelUpSound != null)
        {
            audioSource.PlayOneShot(levelUpSound);
        }

        // Показ выплывающего окна
        if (levelUpPanel != null && levelUpPanelText != null)
        {
            levelUpPanelText.text = $"{level}";
            StartCoroutine(ShowLevelUpPopup());
        }
    }

    private System.Collections.IEnumerator ShowLevelUpPopup()
    {
        levelUpPanel.SetActive(true);

        // Анимация появления (масштабирование)
        CanvasGroup canvasGroup = levelUpPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = levelUpPanel.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        float timer = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 targetScale = Vector3.one;

        // Появление с масштабированием и затуханием
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

        // Задержка перед исчезновением
        yield return new WaitForSeconds(popupDuration / 2);

        // Анимация исчезновения
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