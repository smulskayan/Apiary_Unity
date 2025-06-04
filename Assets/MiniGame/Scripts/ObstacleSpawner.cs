using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform lane1;
    [SerializeField] private RectTransform lane2;
    [SerializeField] private List<GameObject> obstaclePrefabs;
    [SerializeField] private RectTransform obstacleParent;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float moveSpeed = 200f;

    private float timer = 0f;
    private List<RectTransform> activeObstacles = new List<RectTransform>();
    private MiniGameController miniGameController;

    void Start()
    {
        // Проверка и автопоиск MiniGameController
        if (miniGameController == null)
        {
            miniGameController = FindObjectOfType<MiniGameController>();
        }

        if (miniGameController == null)
        {
            Debug.LogError("MiniGameController не найден и не назначен в ObstacleSpawner!");
            enabled = false;
            return;
        }

        if (lane1 == null || lane2 == null)
        {
            Debug.LogError("Lane1 или Lane2 не назначены!");
            enabled = false;
        }
        if (obstaclePrefabs == null || obstaclePrefabs.Count == 0)
        {
            Debug.LogError("Список obstaclePrefabs пуст или не назначен!");
            enabled = false;
        }
        if (obstacleParent == null)
        {
            Debug.LogError("ObstacleParent не назначен!");
            enabled = false;
        }
    }

    void Update()
    {
        if (!enabled || !miniGameController.IsGameActive) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObstacle();
        }

        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            RectTransform rt = activeObstacles[i];
            rt.anchoredPosition -= new Vector2(moveSpeed * Time.deltaTime, 0);

            if (rt.anchoredPosition.x < -obstacleParent.rect.width - 50f)
            {
                RemoveObstacle(rt);
            }
        }
    }

    void SpawnObstacle()
    {
        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
        RectTransform lane = Random.value > 0.5f ? lane1 : lane2;

        GameObject instance = Instantiate(prefab, obstacleParent);
        RectTransform rt = instance.GetComponent<RectTransform>();

        if (rt == null)
        {
            Debug.LogError("Префаб препятствия должен иметь RectTransform!");
            Destroy(instance);
            return;
        }

        rt.anchorMin = new Vector2(0, 0.5f);
        rt.anchorMax = new Vector2(0, 0.5f);
        rt.pivot = new Vector2(0, 0.5f);
        rt.localScale = Vector3.one;

        float startX = obstacleParent.rect.width / 2f + 50f;
        float posY = lane.anchoredPosition.y;

        rt.anchoredPosition = new Vector2(startX, posY);
        activeObstacles.Add(rt);
    }

    public List<RectTransform> GetActiveObstacles()
    {
        return activeObstacles;
    }

    public void RemoveObstacle(RectTransform obstacle)
    {
        if (activeObstacles.Contains(obstacle))
        {
            activeObstacles.Remove(obstacle);
            Destroy(obstacle.gameObject);
        }
    }

    // Добавлено для назначения извне, если нужно
    public void SetController(MiniGameController controller)
    {
        miniGameController = controller;
    }
}
