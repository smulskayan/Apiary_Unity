using UnityEngine;
using System.Collections;

public class Hive : MonoBehaviour
{
    public GameObject beePrefab;
    [SerializeField] private GameObject honeyIcon; // ������ �������� ���
    private int maxBees = 3;
    private int currentBees = 0;
    [SerializeField] private bool hasHoney = false; // ���� ������� ���
    private float honeyProductionTime = 5f; // ����� ������������ ��� (30 ������)
    private bool playerInRange = false; // ���� ���������� ������� �����

    void Start()
    {
        if (beePrefab == null)
        {
            Debug.LogWarning("Bee Prefab not assigned in " + gameObject.name);
        }
        if (honeyIcon == null)
        {
            Debug.LogWarning("HoneyIcon not assigned in " + gameObject.name);
        }
        UpdateHoneyIcon(); // ������������� ��������� ��������� ������ ���

        Crop.OnNectarReady += SpawnBee;
    }

    void Update()
    {
        // �������� ������� ������� E ��� ����� ���
        if (playerInRange && hasHoney && Input.GetKeyDown(KeyCode.E))
        {
            CollectHoney();
            Debug.Log("Key E pressed, attempting to collect honey");
        }
    }

    void OnDestroy()
    {
        Crop.OnNectarReady -= SpawnBee;
    }

    void OnValidate()
    {
        // ��������� ������ ��� ��� ��������� hasHoney � ����������
        UpdateHoneyIcon();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Bear entered hive trigger");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Bear exited hive trigger");
        }
    }

    private void SpawnBee(Crop crop)
    {
        if (currentBees < maxBees && beePrefab != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0.2f, 0, 0);
            GameObject bee = Instantiate(beePrefab, spawnPosition, Quaternion.identity);
            Bee beeScript = bee.GetComponent<Bee>();
            if (beeScript != null)
            {
                beeScript.Initialize(crop.transform, transform);
                currentBees++;
            }
        }
    }

    public void BeeReturned()
    {
        currentBees--;
        if (!hasHoney)
        {
            StartCoroutine(ProduceHoney());
        }
        XPManager xp = FindObjectOfType<XPManager>();
        if (xp != null)
        {
            xp.AddXP(3); // +3 XP �� ���� �������
        }
    }

    private IEnumerator ProduceHoney()
    {
        yield return new WaitForSeconds(honeyProductionTime);
        hasHoney = true;
        UpdateHoneyIcon(); // ��������� ������ ���
        Debug.Log("̸� ����������� � ���� " + gameObject.name);
    }

    public void CollectHoney()
    {
        Item item_honey = new Item("jarHoney", "jarHoney", 1, Item.TYPEPFOOD, 10, 1, 2f);
        Item item_nectar = new Item("nectar", "nectar", 1, Item.TYPEPFOOD, 10, 1, 5f);
        if (hasHoney)
        {
            hasHoney = false;
            Storage storage = FindObjectOfType<Storage>();
            if (storage != null)
            {
                storage.AddHoney(1); // ��������� 1 ������� ��� �� �����
                item_nectar.count = -1;
                Player.checkIfItemExists(item_nectar);
                Debug.Log("Honey collected and added to storage");
            }
            else
            {
                Debug.LogWarning("Storage not found in scene");
            }
            UpdateHoneyIcon(); // ��������� ������ ���
            XPManager xp = FindObjectOfType<XPManager>();
            if (xp != null)
            {
                xp.AddXP(1); // +4 XP �� ���� ���
            }
            item_honey.count = 1;
            Player.checkIfItemExists(item_honey);
        }
        else
        {
            Debug.Log("CollectHoney called, but no honey available");
        }
    }

    private void UpdateHoneyIcon()
    {
        if (honeyIcon != null)
        {
            honeyIcon.SetActive(hasHoney);
            Debug.Log($"UpdateHoneyIcon: hasHoney={hasHoney}, honeyIcon active={honeyIcon.activeSelf}");
        }
        else
        {
            Debug.LogWarning("HoneyIcon is null, cannot update icon state");
        }
    }
}