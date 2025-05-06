using UnityEngine;

public class Bee : MonoBehaviour
{
    private enum BeeState { FlyingToFlower, CollectingNectar, ReturningToHive }
    private BeeState state = BeeState.FlyingToFlower;
    private GameObject hive; // ����
    private GameObject targetFlower; // ������� ������
    private float speed = 5f; // �������� ������
    private float collectTime = 1f; // ����� ����� �������
    private float timer; // ������ ��� �����
    private Vector3 randomOffset; // ��������� ���������� ��� ���������� ������

    void Start()
    {
        hive = GameObject.Find("Hive"); // ������� ����
        FindRandomFlower(); // ���� ��������� ������
        randomOffset = Random.insideUnitSphere * 0.5f; // ��������� ��������� ����������
    }

    void Update()
    {
        switch (state)
        {
            case BeeState.FlyingToFlower:
                if (targetFlower != null)
                {
                    // �������� � ������ � ��������� ��������� �����������
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
                    // ���� ������ ������, ���� �����
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
                    randomOffset = Random.insideUnitSphere * 0.5f; // ����� ��������� ������ ��� ��������
                }
                break;

            case BeeState.ReturningToHive:
                // �������� � ����
                Vector3 hivePos = hive.transform.position + randomOffset;
                transform.position = Vector3.MoveTowards(transform.position, hivePos, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, hivePos) < 0.1f)
                {
                    // ����� ��������� � ����, ���������� � (��� ����� ����������������)
                    Destroy(gameObject);
                }
                break;
        }
    }

    // ����� ���������� ������ � ��������
    private void FindRandomFlower()
    {
        GameObject[] flowers = GameObject.FindGameObjectsWithTag("Flower");
        // ��������� ������ � ��������
        var validFlowers = System.Array.FindAll(flowers, f => f.GetComponent<Flower>().hasNectar);
        if (validFlowers.Length > 0)
        {
            targetFlower = validFlowers[Random.Range(0, validFlowers.Length)];
        }
        else
        {
            // ���� ��� ������� � ��������, ������������ � ����
            state = BeeState.ReturningToHive;
        }
    }
}