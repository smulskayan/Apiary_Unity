using UnityEngine;

public class Flower : MonoBehaviour
{
    public bool hasNectar = true; // Есть ли нектар

    // Пчела забирает нектар
    public void CollectNectar()
    {
        if (hasNectar)
        {
            hasNectar = false;
            // Визуально убираем нектар (например, отключаем объект или меняем цвет)
            gameObject.GetComponent<Renderer>().material.color = Color.gray; // Пример: цветок становится серым
            // Альтернатива: gameObject.SetActive(false); // Цветок исчезает
        }
    }
}