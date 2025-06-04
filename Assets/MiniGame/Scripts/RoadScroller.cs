using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 200f; // Скорость прокрутки (пикселей/сек), настраивается в инспекторе
    private RectTransform rectTransform;
    private float width;
    private bool isScrolling = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform не найден на объекте " + gameObject.name);
            return;
        }

        width = rectTransform.sizeDelta.x; // Ширина изображения дороги
        rectTransform.anchoredPosition = Vector2.zero; // Начальная позиция дороги
    }

    void Update()
    {
        if (!isScrolling || rectTransform == null) return;

        Vector2 pos = rectTransform.anchoredPosition;
        pos.x -= scrollSpeed * Time.deltaTime; // Двигаем влево

        // Бесконечная прокрутка: когда дорога уходит за левую границу, возвращаем её вправо
        if (pos.x <= -width / 2)
        {
            pos.x += width;
        }

        rectTransform.anchoredPosition = pos;
    }

    // Публичный метод для запуска прокрутки
    public void StartScrolling()
    {
        isScrolling = true;
        Debug.Log("RoadScroller: Прокрутка дороги началась");
    }

    // Публичный метод для остановки прокрутки
    public void StopScrolling()
    {
        isScrolling = false;
        Debug.Log("RoadScroller: Прокрутка дороги остановлена");
    }
}