using UnityEngine;
using UnityEngine.UI;

public class IconAnimator : MonoBehaviour
{
    [SerializeField] private GameObject[] icons;
    [SerializeField] private Button honeyButton;
    private Hive hive;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        hive = GetComponentInParent<Hive>();
        if (hive == null)
        {
            Debug.LogWarning("Hive not found in parent of IconAnimator", this);
        }

        if (icons.Length != 4)
        {
            Debug.LogWarning("IconAnimator expects exactly 4 icons", this);
        }

        if (honeyButton != null)
        {
            honeyButton.onClick.AddListener(OnHoneyButtonClicked);
            honeyButton.interactable = false; // Изначально некликабельна
            Debug.Log("Honey button initialized, visibility controlled by animation, interactable: false");
        }
        else
        {
            Debug.LogWarning("Honey Button not assigned in Inspector", this);
        }

        HideIcons(); // Запускаем анимацию скрытия иконок
    }

    public void ShowIcons()
    {
        if (animator != null)
        {
            animator.SetTrigger("SlideIn");
            Debug.Log("ShowIcons triggered, SlideIn animation started, should show all icons including honeyButton");
        }
        else
        {
            Debug.LogWarning("Animator is null, cannot trigger SlideIn", this);
        }
    }

    public void HideIcons()
    {
        if (animator != null)
        {
            animator.SetTrigger("SlideOut");
            Debug.Log("HideIcons triggered, SlideOut animation started, should hide all icons including honeyButton");
        }
        else
        {
            Debug.LogWarning("Animator is null, cannot trigger SlideOut", this);
        }
    }

    public void SetHoneyInteractable(bool hasHoney, bool playerInRange)
    {
        if (honeyButton != null)
        {
            honeyButton.interactable = hasHoney && playerInRange; // Кликабельна для визуальной обратной связи
            Debug.Log($"SetHoneyInteractable: hasHoney={hasHoney}, playerInRange={playerInRange}, interactable={honeyButton.interactable}");
        }
        else
        {
            Debug.LogWarning("HoneyButton is null, cannot set interactable state", this);
        }
    }

    public void TryCollectHoney()
    {
        OnHoneyButtonClicked();
    }

    private void OnHoneyButtonClicked()
    {
        // Кнопка больше не собирает мёд, только для визуального эффекта
        Debug.Log("Honey button clicked, no action (use E key to collect honey)");
    }

    void OnDestroy()
    {
        if (honeyButton != null)
        {
            honeyButton.onClick.RemoveListener(OnHoneyButtonClicked);
        }
    }
}