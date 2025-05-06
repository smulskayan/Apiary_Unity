using UnityEngine;

public class Bee : MonoBehaviour
{
    private enum BeeState { FlyingToFlower, CollectingNectar, ReturningToHive }
    private BeeState state = BeeState.FlyingToFlower;
    private GameObject hive; // Улей
    private GameObject targetFlower; // Целевой цветок
    private float speed = 5f; // Скорость полета
    private float collectTime = 1f; // Время сбора нектара
    private float timer; // Таймер для сбора
    private Vector3 randomOffset; // Случайное отклонение для рандомного полета

    void Start()
    {
        hive = GameObject.Find("Hive"); // Находим улей
        FindRandomFlower(); // Ищем случайный цветок
        randomOffset = Random.insideUnitSphere * 0.5f; // Небольшое случайное отклонение
    }

    void Update()
    {
        switch (state)
        {
            case BeeState.FlyingToFlower:
                if (targetFlower != null)
                {
                    // Движение к цветку с небольшим случайным отклонением
                    Vector3 targetPos = targetFlower.transform.position + randomOffset;
                    transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                    {
                        state = BeeState.CollectingNectar;
                        timer = collectTime;
                    }
                }
                else
                {
                    // Если цветок пропал, ищем новый
                    FindRandomFlower();
                }
                break;

            case BeeState.CollectingNectar:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    if (targetFlower != null)
                    {
                        targetFlower.GetComponent<Flower>().CollectNectar();
                    }
                    state = BeeState.ReturningToHive;
                    randomOffset = Random.insideUnitSphere * 0.5f; // Новый случайный оффсет для возврата
                }
                break;

            case BeeState.ReturningToHive:
                // Движение к улью
                Vector3 hivePos = hive.transform.position + randomOffset;
                transform.position = Vector3.MoveTowards(transform.position, hivePos, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, hivePos) < 0.1f)
                {
                    // Пчела вернулась в улей, уничтожаем её (или можно переиспользовать)
                    Destroy(gameObject);
                }
                break;
        }
    }

    // Поиск случайного цветка с нектаром
    private void FindRandomFlower()
    {
        GameObject[] flowers = GameObject.FindGameObjectsWithTag("Flower");
        // Фильтруем цветки с нектаром
        var validFlowers = System.Array.FindAll(flowers, f => f.GetComponent<Flower>().hasNectar);
        if (validFlowers.Length > 0)
        {
            targetFlower = validFlowers[Random.Range(0, validFlowers.Length)];
        }
        else
        {
            // Если нет цветков с нектаром, возвращаемся в улей
            state = BeeState.ReturningToHive;
        }
    }
}