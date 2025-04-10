using UnityEngine;

public class HiveSlot : MonoBehaviour
{
    public bool isOccupied = false;  // Проверка, занят ли слот
    private SpriteRenderer sr;       // Для изменения цвета
    private Color defaultColor;      // Исходный цвет
    private Color highlightColor = Color.yellow;  // Цвет подсветки

    private bool isNearPlayer = false;  // Определяет, рядом ли игрок

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();  // Получаем компонент SpriteRenderer
        defaultColor = sr.color;  // Сохраняем исходный цвет слота
    }

    void Update()
    {
        // Если игрок рядом и слот не занят, меняем цвет на жёлтый
        if (isNearPlayer && !isOccupied)
        {
            sr.color = highlightColor;  // Подсвечиваем слот
        }
        else
        {
            sr.color = defaultColor;  // Восстанавливаем исходный цвет
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOccupied)
        {
            isNearPlayer = true;  // Игрок рядом
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;  // Игрок покидает слот
        }
    }
}
