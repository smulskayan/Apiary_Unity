using UnityEngine;

public class BeeHive : MonoBehaviour
{
    public GameObject bee; // Префаб пчелы
    public float spawnInterval = 3f; // Интервал спавна (секунды)
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            // Спавним пчелу в позиции улья
            Instantiate(bee, transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}