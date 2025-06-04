using UnityEngine;

public class Colum : MonoBehaviour
{
    public float speed;

    void Update()
    {
        if (MiniGameManagerBee.Instance != null && MiniGameManagerBee.Instance.isGameStarted)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }  
}