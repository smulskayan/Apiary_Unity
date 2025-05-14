using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    public Transform playerCamera; // ������ ������
    public float distanceFromCamera = 5f; // ���������� �� ������ �� Canvas
    public Vector3 offset; // �������������� ��������, ���� �����

    void Start()
    {
        // ���� ������ � ������ ���, ��������� ����� ���
        if (playerCamera == null)
        {
            playerCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        // ������������� ������� Canvas ������������ ������ � ������ ��������
        Vector3 targetPosition = playerCamera.position + playerCamera.forward * distanceFromCamera + offset;

        // ������������� ������� Canvas
        transform.position = targetPosition;

        // ��������� Canvas � ������, ���� �����
        transform.LookAt(playerCamera); // ������������ Canvas ���, ����� �� ������ ������� � ������� ������
    }
}

