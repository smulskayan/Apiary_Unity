using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    public Transform playerCamera; //  амера игрока
    public float distanceFromCamera = 5f; // –ассто€ние от камеры до Canvas
    public Vector3 offset; // ƒополнительное смещение, если нужно

    void Start()
    {
        // ≈сли игрока и камеры нет, попробуем найти его
        if (playerCamera == null)
        {
            playerCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        // ”станавливаем позицию Canvas относительно камеры с учетом смещени€
        Vector3 targetPosition = playerCamera.position + playerCamera.forward * distanceFromCamera + offset;

        // ”станавливаем позицию Canvas
        transform.position = targetPosition;

        // ѕовернуть Canvas к камере, если нужно
        transform.LookAt(playerCamera); // ѕоворачивает Canvas так, чтобы он всегда смотрел в сторону камеры
    }
}

