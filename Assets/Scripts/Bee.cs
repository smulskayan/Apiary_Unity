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
    private AudioSource flyAudioSource; // ������ �� AudioSource ��� ����� ������
    public AudioClip flyClip; // ��������� ��� ����� ������

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

        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("BeeFly");
        }

        flyAudioSource = GetComponent<AudioSource>(); // �������� AudioSource
        if (flyAudioSource != null && flyClip != null)
        {
            flyAudioSource.clip = flyClip;
            flyAudioSource.loop = true; // ����������� ���� ������
            flyAudioSource.Play(); // �������� ���������������
        }

        lastPosition = transform.position;
    }

    void Update()
    {
        Vector2 direction = Vector2.zero;
        if (state == BeeState.MovingToFlower && targetFlower != null)
        {
            direction = (targetFlower.position - transform.position).normalized;
        }
        else if (state == BeeState.ReturningToHive && hive != null)
        {
            direction = (hive.position - transform.position).normalized;
        }

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
                        if (flyAudioSource != null && flyAudioSource.isPlaying)
                        {
                            flyAudioSource.Stop(); // ������������� ���� ��� ����� �������
                        }
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
                    if (flyAudioSource != null && !flyAudioSource.isPlaying)
                    {
                        flyAudioSource.Play(); // ������������ ���� ��� �����������
                    }
                }
                break;

            case BeeState.ReturningToHive:
                if (hive != null)
                {
                    MoveTowards(hive.position);
                    if (Vector2.Distance(transform.position, hive.position) < 0.1f)
                    {
                        hive.SendMessage("BeeReturned", SendMessageOptions.DontRequireReceiver);
                        if (flyAudioSource != null && flyAudioSource.isPlaying)
                        {
                            flyAudioSource.Stop(); // ������������� ���� ����� ������������
                        }
                        Destroy(gameObject);
                    }
                }
                else
                {
                    if (flyAudioSource != null && flyAudioSource.isPlaying)
                    {
                        flyAudioSource.Stop(); // ������������� ���� ����� ������������
                    }
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
            if (direction.x < -0.01f)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x > 0.01f)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}