using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator; // ������ �� Animator
    private SpriteRenderer spriteRenderer; // ������ �� SpriteRenderer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // �������� SpriteRenderer
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // ������������� �������� Speed ��� ��������
        animator.SetFloat("Speed", moveInput.magnitude);

        // ���������� ����������� �������
        if (moveInput.x != 0) // �������� ����������� ������ ��� ��������
        {
            spriteRenderer.flipX = moveInput.x < 0; // �����: flipX = true, ������: flipX = false
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}