using UnityEngine;

public class UIScrollingRoad : MonoBehaviour
{
    [Header("�������� ������ (��� ����� ������ �������)")]
    public RectTransform roadImage1;
    public RectTransform roadImage2;

    [Header("�������� ��������� (�������/���)")]
    public float scrollSpeed = 200f;
    public float spacingOffset = -100f;

    private float imageWidth;

    void Start()
    {
        if (roadImage1 == null || roadImage2 == null)
        {
            Debug.LogError("������� ��� �������� ������!");
            enabled = false;
            return;
        }

        imageWidth = roadImage1.rect.width;
    }

    void Update()
    {
        float moveX = scrollSpeed * Time.deltaTime;

        // ������� ����������� �����
        roadImage1.anchoredPosition -= new Vector2(moveX, 0);
        roadImage2.anchoredPosition -= new Vector2(moveX, 0);

        // �������� � ���� ���� �� ������ ������ �����, ��������� ������ �� ������ ��������
        if (roadImage1.anchoredPosition.x <= -imageWidth)
        {
            roadImage1.anchoredPosition = new Vector2(
                roadImage2.anchoredPosition.x + imageWidth + spacingOffset,
                roadImage1.anchoredPosition.y
            );
        }

        if (roadImage2.anchoredPosition.x <= -imageWidth)
        {
            roadImage2.anchoredPosition = new Vector2(
                roadImage1.anchoredPosition.x + imageWidth + spacingOffset,
                roadImage2.anchoredPosition.y
            );
        }
    }
}