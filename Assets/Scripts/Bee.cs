using UnityEngine;

public class Bee : MonoBehaviour
{
    private enum BeeState { MovingToFlower, CollectingNectar, ReturningToHive, Idle }
    private BeeState state = BeeState.Idle;
    private Transform targetFlower;
    private Transform hive;
    private float speed = 2f;
    private float collectTime = 2f;
    private float timer = 0f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector3 lastPosition;

    public void Initialize(Transform flower, Transform hive)
    {
        this.targetFlower = flower;
        this.hive = hive;
        state = BeeState.MovingToFlower;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = 10;
        }
        else
        {
            Debug.LogError("SpriteRenderer missing on Bee", this);
        }

        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("BeeFly");
        }
        else
        {
            Debug.LogWarning("Animator component missing on Bee");
        }

        lastPosition = transform.position;
    }

    void Update()
    {
        // Вычисляем направление на основе текущей цели
        Vector2 direction = Vector2.zero;
        if (state == BeeState.MovingToFlower && targetFlower != null)
        {
            direction = (targetFlower.position - transform.position).normalized;
        }
        else if (state == BeeState.ReturningToHive && hive != null)
        {
            direction = (hive.position - transform.position).normalized;
        }

        // Обновляем зеркальное отражение
        UpdateSpriteFlip(direction);

        switch (state)
        {
            case BeeState.MovingToFlower:
                if (targetFlower != null)
                {
                    MoveTowards(targetFlower.position);
                    if (Vector2.Distance(transform.position, targetFlower.position) < 0.1f)
                    {
                        state = BeeState.CollectingNectar;
                        timer = 0f;
                    }
                }
                else
                {
                    state = BeeState.ReturningToHive;
                }
                break;

            case BeeState.CollectingNectar:
                timer += Time.deltaTime;
                if (timer >= collectTime)
                {
                    if (targetFlower != null)
                    {
                        Crop crop = targetFlower.GetComponent<Crop>();
                        if (crop != null)
                        {
                            crop.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
                        }
                    }
                    state = BeeState.ReturningToHive;
                }
                break;

            case BeeState.ReturningToHive:
                if (hive != null)
                {
                    MoveTowards(hive.position);
                    if (Vector2.Distance(transform.position, hive.position) < 0.1f)
                    {
                        hive.SendMessage("BeeReturned", SendMessageOptions.DontRequireReceiver);
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
        }

        lastPosition = transform.position;
    }

    private void MoveTowards(Vector3 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void UpdateSpriteFlip(Vector2 direction)
    {
        if (spriteRenderer != null)
        {
            // Логируем направление для отладки
            Debug.Log($"Bee direction: x={direction.x}, current flipX={spriteRenderer.flipX}", this);

            if (direction.x < -0.01f) // Небольшой порог для исключения мелких колебаний
            {
                spriteRenderer.flipX = true; // Смотрим влево
            }
            else if (direction.x > 0.01f)
            {
                spriteRenderer.flipX = false; // Смотрим вправо
            }
        }
        else
        {
            Debug.LogError("SpriteRenderer is null in UpdateSpriteFlip", this);
        }
    }
}