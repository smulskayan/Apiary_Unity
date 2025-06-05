using UnityEngine;

public class HiveSlot : MonoBehaviour
{
    public bool isOccupied = false;
    public int requiredLevel = 1;
    public GameObject lockSprite;
    private SpriteRenderer sr;
    private Color defaultColor;
    private Color highlightColor = Color.yellow;
    private bool isNearPlayer = false;
    private XPManager xpManager;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;
        xpManager = FindFirstObjectByType<XPManager>();
        UpdateLockSprite();
    }

    void Update()
    {
        UpdateLockSprite();

        if (isNearPlayer && !isOccupied && IsSlotUnlocked())
        {
            sr.color = highlightColor;
        }
        else
        {
            sr.color = defaultColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOccupied && IsSlotUnlocked())
        {
            isNearPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }

    private bool IsSlotUnlocked()
    {
        return xpManager != null && xpManager.level >= requiredLevel;
    }

    private void UpdateLockSprite()
    {
        if (lockSprite != null)
        {
            lockSprite.SetActive(!IsSlotUnlocked());
        }
    }
}