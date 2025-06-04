using UnityEngine;

public class UIScrollingRoad : MonoBehaviour
{
    [Header("Картинки дороги (две копии одного спрайта)")]
    public RectTransform roadImage1;
    public RectTransform roadImage2;

    [Header("Скорость прокрутки (пиксели/сек)")]
    public float scrollSpeed = 200f;
    public float spacingOffset = -100f;

    private float imageWidth;

    void Start()
    {
        if (roadImage1 == null || roadImage2 == null)
        {
            Debug.LogError("Назначь обе картинки дороги!");
            enabled = false;
            return;
        }

        imageWidth = roadImage1.rect.width;
    }

    void Update()
    {
        float moveX = scrollSpeed * Time.deltaTime;

        // Двигаем изображения влево
        roadImage1.anchoredPosition -= new Vector2(moveX, 0);
        roadImage2.anchoredPosition -= new Vector2(moveX, 0);

        // Проверка — если ушла за предел экрана слева, перенести вправо за другую картинку
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