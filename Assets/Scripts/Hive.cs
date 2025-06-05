using UnityEngine;
using System.Collections;

public class Hive : MonoBehaviour
{
    public GameObject beePrefab;
    public GameObject honeyIcon;
    private int maxBees = 1;
    private int currentBees = 0;
    [SerializeField] private bool hasHoney = false;
    private float honeyProductionTime = 5f;
    private bool playerInRange = false;

    void Start()
    {
        UpdateHoneyIcon();
        Crop.OnNectarReady += SpawnBee;
    }

    void Update()
    {
        if (playerInRange && hasHoney && Input.GetKeyDown(KeyCode.E)) {
            CollectHoney();
        }
    }

    void OnDestroy()
    {
        Crop.OnNectarReady -= SpawnBee;
    }

    void OnValidate()
    {
        UpdateHoneyIcon();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            playerInRange = false;
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
        if (!hasHoney) {
            StartCoroutine(ProduceHoney());
        }
        XPManager xp = FindFirstObjectByType<XPManager>();
        if (xp != null) {
            xp.AddXP(3);
        }
    }

    private IEnumerator ProduceHoney()
    {
        yield return new WaitForSeconds(honeyProductionTime);
        hasHoney = true;
        UpdateHoneyIcon();
    }

    public void CollectHoney()
    {
        Item item_honey = new Item("jar_honey", "jar_honey", 1, Item.TYPEPFOOD, 10, 1, 2f);
        Item item_nectar = new Item("nectar", "nectar", 1, Item.TYPEPFOOD, 10, 1, 5f);
        if (hasHoney)
        {
            hasHoney = false;
            item_nectar.count = -1;
            Player.checkIfItemExists(item_nectar);
            UpdateHoneyIcon();
            XPManager xp = FindFirstObjectByType<XPManager>();
            if (xp != null) {
                xp.AddXP(1);
            }
            item_honey.count = 1;
            Player.checkIfItemExists(item_honey);
        }
    }

    private void UpdateHoneyIcon()
    {
        if (honeyIcon != null) {
            honeyIcon.SetActive(hasHoney);
        }
    }
}