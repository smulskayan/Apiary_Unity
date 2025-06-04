using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 200f; // �������� ��������� (��������/���), ������������� � ����������
    private RectTransform rectTransform;
    private float width;
    private bool isScrolling = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform �� ������ �� ������� " + gameObject.name);
            return;
        }

        width = rectTransform.sizeDelta.x; // ������ ����������� ������
        rectTransform.anchoredPosition = Vector2.zero; // ��������� ������� ������
    }

    void Update()
    {
        if (!isScrolling || rectTransform == null) return;

        Vector2 pos = rectTransform.anchoredPosition;
        pos.x -= scrollSpeed * Time.deltaTime; // ������� �����

        // ����������� ���������: ����� ������ ������ �� ����� �������, ���������� � ������
        if (pos.x <= -width / 2)
        {
            pos.x += width;
        }

        rectTransform.anchoredPosition = pos;
    }

    // ��������� ����� ��� ������� ���������
    public void StartScrolling()
    {
        isScrolling = true;
        Debug.Log("RoadScroller: ��������� ������ ��������");
    }

    // ��������� ����� ��� ��������� ���������
    public void StopScrolling()
    {
        isScrolling = false;
        Debug.Log("RoadScroller: ��������� ������ �����������");
    }
}