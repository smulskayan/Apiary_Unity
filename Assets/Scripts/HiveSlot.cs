using UnityEngine;

public class HiveSlot : MonoBehaviour
{
    public bool isOccupied = false;
    private SpriteRenderer sr;
    private Color defaultColor;
    private Color targetColor;

    private bool isNearPlayer = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;
        targetColor = defaultColor;
    }

    void Update()
    {
        sr.color = Color.Lerp(sr.color, targetColor, 5f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOccupied)
        {
            isNearPlayer = true;
            HighlightNearestSlot();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
            targetColor = defaultColor;
        }
    }

    void HighlightNearestSlot()
    {
        HiveSlot[] allSlots = FindObjectsOfType<HiveSlot>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        HiveSlot nearest = null;
        float minDist = float.MaxValue;

        foreach (HiveSlot slot in allSlots)
        {
            float dist = Vector2.Distance(slot.transform.position, player.transform.position);
            if (!slot.isOccupied && dist < minDist)
            {
                minDist = dist;
                nearest = slot;
            }
        }

        foreach (HiveSlot slot in allSlots)
        {
            slot.targetColor = (slot == nearest) ? Color.yellow : slot.defaultColor;
        }
    }
}
