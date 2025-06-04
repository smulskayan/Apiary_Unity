using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MiniGameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Image carSprite;
    [SerializeField] private RectTransform lane1;
    [SerializeField] private RectTransform lane2;
    [SerializeField] private GameObject miniGameObjects;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private float scrollSpeed = 200f;

    private bool isGameActive = false;
    private int currentLane = 1;

    public bool IsGameActive => isGameActive;

    void Start()
    {
        // Если ссылка не назначена в инспекторе — ищем её автоматически
        if (obstacleSpawner == null)
        {
            obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
            if (obstacleSpawner == null)
            {
                Debug.LogError("ObstacleSpawner не найден в сцене и не назначен в MiniGameController!");
                enabled = false;
                return;
            }
        }

        if (countdownText == null) Debug.LogError("CountdownText не назначен в MiniGameController!");
        if (carSprite == null) Debug.LogError("CarSprite не назначен в MiniGameController!");
        if (lane1 == null) Debug.LogError("Lane1 не назначен в MiniGameController!");
        if (lane2 == null) Debug.LogError("Lane2 не назначен в MiniGameController!");
        if (miniGameObjects == null) Debug.LogError("MiniGameObjects не назначен в MiniGameController!");
        if (playerCamera == null) Debug.LogError("PlayerCamera не назначена в MiniGameController!");

        Debug.Log("MiniGameController: Инициализация завершена");

        miniGameObjects.SetActive(false);
        playerCamera.enabled = false;
    }

    // остальной код без изменений...
    // ...

    private void CheckCollisions()
    {
        if (obstacleSpawner == null) return;

        Rect carRect = GetWorldRect(carSprite.rectTransform);

        // Копируем список активных препятствий, чтобы избежать ошибки при изменении коллекции
        List<RectTransform> obstacles = new List<RectTransform>(obstacleSpawner.GetActiveObstacles());

        foreach (RectTransform obstacle in obstacles)
        {
            Rect obstacleRect = GetWorldRect(obstacle);
            if (carRect.Overlaps(obstacleRect))
            {
                if (obstacle.CompareTag("Honey"))
                {
                    Debug.Log("Собран мед!");
                    obstacleSpawner.RemoveObstacle(obstacle);
                }
                else if (obstacle.CompareTag("Barrel"))
                {
                    Debug.Log("Столкновение с бочкой!");
                    // Добавьте логику урона или проигрыша
                }
            }
        }
    }

    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return new Rect(corners[0].x, corners[0].y,
                        corners[2].x - corners[0].x,
                        corners[2].y - corners[0].y);
    }

    public void ResetGame()
    {
        isGameActive = false;
        if (carSprite != null && lane1 != null)
        {
            carSprite.rectTransform.anchoredPosition = new Vector2(-200, lane1.anchoredPosition.y);
        }
        currentLane = 1;
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }
        if (carSprite != null)
        {
            carSprite.gameObject.SetActive(false);
        }
        miniGameObjects.SetActive(false);
        playerCamera.enabled = false;
        Debug.Log("MiniGameController: Состояние игры сброшено");
    }
}
