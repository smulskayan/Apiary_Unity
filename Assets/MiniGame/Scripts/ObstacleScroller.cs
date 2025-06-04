//using UnityEngine;
//using System.Collections.Generic;

//public class ObstacleS : MonoBehaviour
//{
//    [SerializeField] private RectTransform lane1; // Верхняя полоса
//    [SerializeField] private RectTransform lane2; // Нижняя полоса
//    [SerializeField] private List<GameObject> obstaclePrefabs; // Префабы меда и бочки
//    [SerializeField] private RectTransform obstacleParent; // Родительский объект для препятствий
//    [SerializeField] private MiniGameController gameController; // Ссылка на контроллер игры
//    [SerializeField] private float spawnInterval = 1f; // Интервал спавна (1 секунда)
//    [SerializeField] private float moveSpeed = 200f; // Скорость движения препятствий (синхронизирована с UIScrollingRoad)

//    private float timer = 0f;
//    private List<RectTransform> activeObstacles = new List<RectTransform>();

//    void Start()
//    {
//        // Проверка корректности настроек
//        if (lane1 == null || lane2 == null)
//        {
//            Debug.LogError("Lane1 или Lane2 не назначены в ObstacleSpawner!");
//            enabled = false;
//        }
//        if (obstaclePrefabs == null || obstaclePrefabs.Count == 0)
//        {
//            Debug.LogError("Список obstaclePrefabs пуст или не назначен в ObstacleSpawner!");
//            enabled = false;
//        }
//        if (obstacleParent == null)
//        {
//            Debug.LogError("ObstacleParent не назначен в ObstacleSpawner!");
//            enabled = false;
//        }
//        if (gameController == null)
//        {
//            Debug.LogError("MiniGameController не назначен в ObstacleSpawner!");
//            enabled = false;
//        }
//    }

//    void Update()
//    {
//        if (!enabled || gameController == null || !gameController.IsGameActive) return;

//        timer += Time.deltaTime;
//        if (timer >= spawnInterval)
//        {
//            timer = 0f;
//            SpawnObstacle();
//        }

//        // Двигаем препятствия влево с той же скоростью, что и дорога
//        for (int i = activeObstacles.Count - 1; i >= 0; i--)
//        {
//            RectTransform rt = activeObstacles[i];
//            rt.anchoredPosition -= new Vector2(moveSpeed * Time.deltaTime, 0);

//            // Удаляем препятствие, если оно вышло за пределы экрана
//            if (rt.anchoredPosition.x < -obstacleParent.rect.width - 50f)
//            {
//                Destroy(rt.gameObject);
//                activeObstacles.RemoveAt(i);
//            }
//        }
//    }

//    void SpawnObstacle()
//    {
//        // Выбор случайного префаба
//        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
//        // Выбор случайной полосы
//        RectTransform lane = Random.value > 0.5f ? lane1 : lane2;

//        // Создаем экземпляр префаба
//        GameObject instance = Instantiate(prefab, obstacleParent);
//        RectTransform rt = instance.GetComponent<RectTransform>();

//        if (rt == null)
//        {
//            Debug.LogError("Префаб препятствия должен иметь RectTransform!");
//            Destroy(instance);
//            return;
//        }

//        // Настройка якорей и позиции
//        rt.anchorMin = new Vector2(0, 0.5f);
//        rt.anchorMax = new Vector2(0, 0.5f);
//        rt.pivot = new Vector2(0, 0.5f);
//        rt.localScale = Vector3.one;

//        // Начальная позиция: правый край родительского RectTransform
//        float startX = obstacleParent.rect.width / 2f + 50f;
//        float posY = lane.anchoredPosition.y;

//        rt.anchoredPosition = new Vector2(startX, posY);
//        activeObstacles.Add(rt);
//    }

//    // Публичный метод для доступа к активным препятствиям (для проверки столкновений)
//    public List<RectTransform> GetActiveObstacles()
//    {
//        return activeObstacles;
//    }
//}