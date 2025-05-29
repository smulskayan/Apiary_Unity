using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private float speed;
    private HoneyCatchMinigame minigame;

    public void Init(HoneyCatchMinigame game, float fallSpeed)
    {
        minigame = game;
        speed = fallSpeed;
        gameObject.tag = gameObject.name.Contains("Honey") ? "Honey" : "Trash";
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            if (gameObject.CompareTag("Honey"))
            {
                minigame.MissHoney();
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Barrel")
        {
            if (gameObject.CompareTag("Honey"))
            {
                minigame.CatchHoney();
            }
            Destroy(gameObject);
        }
    }
}