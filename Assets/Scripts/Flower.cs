using UnityEngine;

public class Flower : MonoBehaviour
{
    public bool hasNectar = true; // ���� �� ������

    // ����� �������� ������
    public void CollectNectar()
    {
        if (hasNectar)
        {
            hasNectar = false;
            // ��������� ������� ������ (��������, ��������� ������ ��� ������ ����)
            gameObject.GetComponent<Renderer>().material.color = Color.gray; // ������: ������ ���������� �����
            // ������������: gameObject.SetActive(false); // ������ ��������
        }
    }
}