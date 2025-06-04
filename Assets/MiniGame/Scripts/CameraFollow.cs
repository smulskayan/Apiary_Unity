using UnityEngine;

public class CameraFollowCar : MonoBehaviour
{
    public Transform target; // Ссылка на машинку
    public float smoothSpeed = 0.125f; // Скорость сглаживания

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}