using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip stepClip;
    public float speedThreshold = 0.1f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool isMoving = rb.linearVelocity.magnitude > speedThreshold;

        if (isMoving)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = stepClip;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
