//using UnityEngine;
//using System.Collections.Generic;

//public class ObstacleS : MonoBehaviour
//{
//    [SerializeField] private RectTransform lane1; // ������� ������
//    [SerializeField] private RectTransform lane2; // ������ ������
//    [SerializeField] private List<GameObject> obstaclePrefabs; // ������� ���� � �����
//    [SerializeField] private RectTransform obstacleParent; // ������������ ������ ��� �����������
//    [SerializeField] private MiniGameController gameController; // ������ �� ���������� ����
//    [SerializeField] private float spawnInterval = 1f; // �������� ������ (1 �������)
//    [SerializeField] private float moveSpeed = 200f; // �������� �������� ����������� (���������������� � UIScrollingRoad)

//    private float timer = 0f;
//    private List<RectTransform> activeObstacles = new List<RectTransform>();

//    void Start()
//    {
//        // �������� ������������ ��������
//        if (lane1 == null || lane2 == null)
//        {
//            Debug.LogError("Lane1 ��� Lane2 �� ��������� � ObstacleSpawner!");
//            enabled = false;
//        }
//        if (obstaclePrefabs == null || obstaclePrefabs.Count == 0)
//        {
//            Debug.LogError("������ obstaclePrefabs ���� ��� �� �������� � ObstacleSpawner!");
//            enabled = false;
//        }
//        if (obstacleParent == null)
//        {
//            Debug.LogError("ObstacleParent �� �������� � ObstacleSpawner!");
//            enabled = false;
//        }
//        if (gameController == null)
//        {
//            Debug.LogError("MiniGameController �� �������� � ObstacleSpawner!");
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

//        // ������� ����������� ����� � ��� �� ���������, ��� � ������
//        for (int i = activeObstacles.Count - 1; i >= 0; i--)
//        {
//            RectTransform rt = activeObstacles[i];
//            rt.anchoredPosition -= new Vector2(moveSpeed * Time.deltaTime, 0);

//            // ������� �����������, ���� ��� ����� �� ������� ������
//            if (rt.anchoredPosition.x < -obstacleParent.rect.width - 50f)
//            {
//                Destroy(rt.gameObject);
//                activeObstacles.RemoveAt(i);
//            }
//        }
//    }

//    void SpawnObstacle()
//    {
//        // ����� ���������� �������
//        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
//        // ����� ��������� ������
//        RectTransform lane = Random.value > 0.5f ? lane1 : lane2;

//        // ������� ��������� �������
//        GameObject instance = Instantiate(prefab, obstacleParent);
//        RectTransform rt = instance.GetComponent<RectTransform>();

//        if (rt == null)
//        {
//            Debug.LogError("������ ����������� ������ ����� RectTransform!");
//            Destroy(instance);
//            return;
//        }

//        // ��������� ������ � �������
//        rt.anchorMin = new Vector2(0, 0.5f);
//        rt.anchorMax = new Vector2(0, 0.5f);
//        rt.pivot = new Vector2(0, 0.5f);
//        rt.localScale = Vector3.one;

//        // ��������� �������: ������ ���� ������������� RectTransform
//        float startX = obstacleParent.rect.width / 2f + 50f;
//        float posY = lane.anchoredPosition.y;

//        rt.anchoredPosition = new Vector2(startX, posY);
//        activeObstacles.Add(rt);
//    }

//    // ��������� ����� ��� ������� � �������� ������������ (��� �������� ������������)
//    public List<RectTransform> GetActiveObstacles()
//    {
//        return activeObstacles;
//    }
//}