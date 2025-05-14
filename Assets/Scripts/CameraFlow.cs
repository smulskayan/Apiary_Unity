using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;    // Ссылка на трансформ игрока
    public Vector3 offset;      // Смещение камеры относительно игрока

    void Start()
    {
        // Если камера не привязана к игроку, то найдем объект с тегом "Player"
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        // Камера следит за игроком с использованием Smooth Lerp
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
    }
}
