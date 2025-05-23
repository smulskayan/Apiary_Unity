using UnityEngine;

public class HiveManager : MonoBehaviour
{
    public GameObject beehivePrefab;  // ������ ����
    public float placementRange = 2f; // ��������� �� �����
    public Vector3 placementOffset = new Vector3(0, 0.5f, 0); // �������� �����

    private GameObject player;        // ����� (�������)

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            if (dist < minDist && dist <= placementRange && !slot.isOccupied)
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

            //  ���������� 3 XP �� ��������� ����
            XPManager xp = FindObjectOfType<XPManager>();
            if (xp != null)
            {
                xp.AddXP(3);
            }
        }
    }
}
