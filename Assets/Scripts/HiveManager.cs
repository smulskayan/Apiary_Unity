using UnityEngine;

public class HiveManager : MonoBehaviour
{
    public GameObject beehivePrefab;
    public float placementRange = 2f;
    public Vector3 placementOffset = new Vector3(0, 0.5f, 0);
    public AudioClip placementSound; // Звук размещения улья
    private AudioSource audioSource; // Ссылка на AudioSource
    private GameObject player;
    private XPManager xpManager; // Ссылка на XPManager

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>(); // Получаем AudioSource
        xpManager = FindObjectOfType<XPManager>(); // Находим XPManager
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPlaceHive();
        }
    }

    void TryPlaceHive()
    {
        HiveSlot[] slots = FindObjectsOfType<HiveSlot>();
        HiveSlot nearest = null;
        float minDist = Mathf.Infinity;

        foreach (HiveSlot slot in slots)
        {
            float dist = Vector2.Distance(player.transform.position, slot.transform.position);
            // Проверяем, не занят ли слот и доступен ли он по уровню
            if (dist < minDist && dist <= placementRange && !slot.isOccupied && IsSlotUnlocked(slot))
            {
                minDist = dist;
                nearest = slot;
            }
        }

        if (nearest != null)
        {
            Vector3 spawnPosition = nearest.transform.position + placementOffset;
            Instantiate(beehivePrefab, spawnPosition, Quaternion.identity);
            nearest.isOccupied = true;

            if (audioSource != null && placementSound != null)
            {
                audioSource.PlayOneShot(placementSound); // Воспроизводим звук размещения
            }

            if (xpManager != null)
            {
                xpManager.AddXP(3);
            }
        }
    }

    private bool IsSlotUnlocked(HiveSlot slot)
    {
        return xpManager != null && xpManager.level >= slot.requiredLevel;
    }
}