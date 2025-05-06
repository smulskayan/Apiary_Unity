using UnityEngine;

public class BeeHive : MonoBehaviour
{
    public GameObject bee; // ������ �����
    public float spawnInterval = 3f; // �������� ������ (�������)
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            // ������� ����� � ������� ����
            Instantiate(bee, transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}