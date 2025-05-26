using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    public Transform playerCamera;
    public float distanceFromCamera = 5f;
    public Vector3 offset;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        Vector3 targetPosition = playerCamera.position + playerCamera.forward * distanceFromCamera + offset;
        transform.position = targetPosition;
        transform.LookAt(playerCamera);
    }
}

