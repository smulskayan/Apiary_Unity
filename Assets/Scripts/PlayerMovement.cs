using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public AudioClip[] stepClips; // Массив звуков шагов
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource stepAudioSource; // Ссылка на AudioSource

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stepAudioSource = GetComponent<AudioSource>(); // Получаем AudioSource
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", moveInput.magnitude);

        if (moveInput.x != 0)
        {
            spriteRenderer.flipX = moveInput.x < 0;
        }

        // Воспроизведение и остановка звука шагов
        if (moveInput.magnitude > 0 && !stepAudioSource.isPlaying)
        {
            stepAudioSource.clip = stepClips[Random.Range(0, stepClips.Length)];
            stepAudioSource.Play();
        }
        else if (moveInput.magnitude == 0 && stepAudioSource.isPlaying)
        {
            stepAudioSource.Stop();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}