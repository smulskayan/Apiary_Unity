using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    private SpriteRenderer sr;
    private Camera mainCam;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        // ÷ентр сортировки Ч по позиции камеры
        float relativeY = transform.position.y - mainCam.transform.position.y;
        sr.sortingOrder = -(int)(relativeY * 100);
    }
}