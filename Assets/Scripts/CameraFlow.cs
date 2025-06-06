using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;    // ������ �� ��������� ������
    public Vector3 offset;      // �������� ������ ������������ ������

    void Start()
    {
        // ���� ������ �� ��������� � ������, �� ������ ������ � ����� "Player"
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        // ������ ������ �� ������� � �������������� Smooth Lerp
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
    }
}
