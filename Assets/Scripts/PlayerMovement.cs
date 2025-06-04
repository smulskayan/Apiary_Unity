using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f; // Скорость движения
    [SerializeField] private AudioClip[] stepClips; // Массив звуков шагов
    [SerializeField] private GameObject miniGameCanvas; // Ссылка на MiniGameCanvas
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
        stepAudioSource = GetComponent<AudioSource>();

        // Проверяем назначение MiniGameCanvas
        if (miniGameCanvas == null)
        {
            Debug.LogError("MiniGameCanvas не назначен в PlayerMovement! Пожалуйста, назначьте Canvas в инспекторе.");
        }
    }

    void Update()
    {
        // Проверяем, активен ли MiniGameCanvas
        if (miniGameCanvas != null && miniGameCanvas.activeSelf)
        {
            // Отключаем движение, анимацию и звук
            moveInput = Vector2.zero;
            animator.SetFloat("Speed", 0);
            if (stepAudioSource.isPlaying)
            {
                stepAudioSource.Stop();
            }
            return;
        }

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
        // Двигаем персонажа только если MiniGameCanvas неактивен
        if (miniGameCanvas == null || !miniGameCanvas.activeSelf)
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        }
    }
}