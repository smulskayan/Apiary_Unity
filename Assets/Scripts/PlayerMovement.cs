using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator; // Ссылка на Animator
    private SpriteRenderer spriteRenderer; // Ссылка на SpriteRenderer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Получаем SpriteRenderer
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Устанавливаем параметр Speed для анимаций
        animator.SetFloat("Speed", moveInput.magnitude);

        // Зеркальное отображение спрайта
        if (moveInput.x != 0) // Изменяем направление только при движении
        {
            spriteRenderer.flipX = moveInput.x < 0; // Влево: flipX = true, вправо: flipX = false
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}