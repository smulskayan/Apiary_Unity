using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f; // �������� ��������
    [SerializeField] private AudioClip[] stepClips; // ������ ������ �����
    [SerializeField] private GameObject miniGameCanvas; // ������ �� MiniGameCanvas
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource stepAudioSource; // ������ �� AudioSource

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stepAudioSource = GetComponent<AudioSource>(); // �������� AudioSource
    }

    void Update()
    {
        // ���������, ������� �� MiniGameCanvas
        if (miniGameCanvas != null && miniGameCanvas.activeSelf)
        {
            // ��������� ��������, �������� � ����
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

        // ��������������� � ��������� ����� �����
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
        // ������� ��������� ������ ���� MiniGameCanvas ���������
        if (miniGameCanvas == null || !miniGameCanvas.activeSelf)
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        }
    }
}