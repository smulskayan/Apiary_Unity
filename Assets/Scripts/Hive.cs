using UnityEngine;
using System.Collections;

public class Hive : MonoBehaviour
{
    public GameObject beePrefab;
    [SerializeField] private GameObject honeyIcon; // Объект картинки мёда
    private int maxBees = 3;
    private int currentBees = 0;
    [SerializeField] private bool hasHoney = false; // Флаг наличия мёда
    private float honeyProductionTime = 5f; // Время производства мёда (30 секунд)
    private bool playerInRange = false; // Флаг нахождения медведя рядом

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
        UpdateHoneyIcon(); // Устанавливаем начальное состояние иконки мёда

        Crop.OnNectarReady += SpawnBee;
    }

    void Update()
    {
        // Проверка нажатия клавиши E для сбора мёда
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
        // Обновляем иконку мёда при изменении hasHoney в инспекторе
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
            xp.AddXP(3); // +3 XP за сбор нектара
        }
    }

    private IEnumerator ProduceHoney()
    {
        yield return new WaitForSeconds(honeyProductionTime);
        hasHoney = true;
        UpdateHoneyIcon(); // Обновляем иконку мёда
        Debug.Log("Мёд заспавнился в улье " + gameObject.name);
    }

    public void CollectHoney()
    {
        if (hasHoney)
        {
            hasHoney = false;
            Storage storage = FindObjectOfType<Storage>();
            if (storage != null)
            {
                storage.AddHoney(1); // Добавляем 1 единицу мёда на склад
                Debug.Log("Honey collected and added to storage");
            }
            else
            {
                Debug.LogWarning("Storage not found in scene");
            }
            UpdateHoneyIcon(); // Обновляем иконку мёда
            XPManager xp = FindObjectOfType<XPManager>();
            if (xp != null)
            {
                xp.AddXP(1); // +4 XP за сбор мёда
            }
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