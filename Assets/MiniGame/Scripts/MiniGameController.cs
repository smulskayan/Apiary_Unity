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
        // ���� ������ �� ��������� � ���������� � ���� � �������������
        if (obstacleSpawner == null)
        {
            obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
            if (obstacleSpawner == null)
            {
                Debug.LogError("ObstacleSpawner �� ������ � ����� � �� �������� � MiniGameController!");
                enabled = false;
                return;
            }
        }

        if (countdownText == null) Debug.LogError("CountdownText �� �������� � MiniGameController!");
        if (carSprite == null) Debug.LogError("CarSprite �� �������� � MiniGameController!");
        if (lane1 == null) Debug.LogError("Lane1 �� �������� � MiniGameController!");
        if (lane2 == null) Debug.LogError("Lane2 �� �������� � MiniGameController!");
        if (miniGameObjects == null) Debug.LogError("MiniGameObjects �� �������� � MiniGameController!");
        if (playerCamera == null) Debug.LogError("PlayerCamera �� ��������� � MiniGameController!");

        Debug.Log("MiniGameController: ������������� ���������");

        miniGameObjects.SetActive(false);
        playerCamera.enabled = false;
    }

    // ��������� ��� ��� ���������...
    // ...

    private void CheckCollisions()
    {
        if (obstacleSpawner == null) return;

        Rect carRect = GetWorldRect(carSprite.rectTransform);

        // �������� ������ �������� �����������, ����� �������� ������ ��� ��������� ���������
        List<RectTransform> obstacles = new List<RectTransform>(obstacleSpawner.GetActiveObstacles());

        foreach (RectTransform obstacle in obstacles)
        {
            Rect obstacleRect = GetWorldRect(obstacle);
            if (carRect.Overlaps(obstacleRect))
            {
                if (obstacle.CompareTag("Honey"))
                {
                    Debug.Log("������ ���!");
                    obstacleSpawner.RemoveObstacle(obstacle);
                }
                else if (obstacle.CompareTag("Barrel"))
                {
                    Debug.Log("������������ � ������!");
                    // �������� ������ ����� ��� ���������
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
        Debug.Log("MiniGameController: ��������� ���� ��������");
    }
}
