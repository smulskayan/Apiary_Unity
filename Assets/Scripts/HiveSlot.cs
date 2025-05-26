using UnityEngine;

public class HiveSlot : MonoBehaviour
{
    public bool isOccupied = false;
    private SpriteRenderer sr;
    private Color defaultColor;
    private Color highlightColor = Color.yellow;

    private bool isNearPlayer = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;
    }

    void Update()
    {
        if (isNearPlayer && !isOccupied) {
            sr.color = highlightColor;
        }
        else {
            sr.color = defaultColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOccupied)
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
}
