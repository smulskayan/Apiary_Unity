using UnityEngine;
using System.Collections;
using System;

public class Crop : MonoBehaviour
{
    public static event Action<Crop> OnNectarReady; // Событие появления нектара

    private int STEP_EMPTY = 0;
    private int STEP_GROWS = 1;
    private int STEP_READY_FLOWER = 2;
    private int STEP_READY_NECTAR = 3;
    private int STEP_WANT_WATERING = 4;

    private SpriteRenderer seedSpriteRenderer;
    private SpriteRenderer flowerSpriteRenderer;
    private SpriteRenderer nectarSpriteRenderer;
    private SpriteRenderer cropSpriteRenderer;
    private SpriteRenderer waterSpriteRenderer;

    private Item cropItem;
    private int step = 0;

    private GameObject player;
    private bool readyForAction;

    void Start()
    {
        cropSpriteRenderer = GetComponent<SpriteRenderer>();
        seedSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[2];
        flowerSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[1];
        nectarSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[3];
        waterSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[4];

        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) TryPlaceCrop();
    }

    void TryPlaceCrop()
    {
        Item item = new Item("flower", "flower", 1, Item.TYPEPFOOD, 10, 1, 5f);

        if (readyForAction)
        {
            if (step == STEP_EMPTY)
            {
                if (item.type == Item.TYPEPFOOD)
                {
                    step = STEP_GROWS;
                    cropItem = item;
                    seedSpriteRenderer.sprite = Resources.Load<Sprite>("seeds");
                    StartCoroutine(grow());
                    XPManager xp = FindObjectOfType<XPManager>();
                    if (xp != null)
                    {
                        xp.AddXP(3); // +3 XP за посадку
                    }
                }
            }
            else if (step == STEP_READY_FLOWER)
            {
                step = STEP_GROWS;
                StartCoroutine(createNectar());
            }
            else if (step == STEP_WANT_WATERING)
            {
                waterSpriteRenderer.sprite = Resources.Load<Sprite>("water");
                StartCoroutine(watringGround());
            }
        }
    }

    void OnMouseDown()
    {
        if (step == STEP_READY_NECTAR)
        {
            step = STEP_GROWS;
            nectarSpriteRenderer.sprite = Resources.Load<Sprite>("empty");
            StartCoroutine(dryGround());
        }
    }

    private IEnumerator grow()
    {
        yield return new WaitForSeconds(cropItem.timeToGrow);
        seedSpriteRenderer.sprite = Resources.Load<Sprite>("empty");
        flowerSpriteRenderer.sprite = Resources.Load<Sprite>(cropItem.imgUrl);
        step = STEP_READY_FLOWER;
    }

    private IEnumerator createNectar()
    {
        yield return new WaitForSeconds(5f);
        nectarSpriteRenderer.sprite = Resources.Load<Sprite>("nectar");
        step = STEP_READY_NECTAR;
        OnNectarReady?.Invoke(this); // Уведомляем о появлении нектара
    }

    private IEnumerator dryGround()
    {
        yield return new WaitForSeconds(5f);
        flowerSpriteRenderer.sprite = Resources.Load<Sprite>("flower_dead");
        step = STEP_WANT_WATERING;
    }

    private IEnumerator watringGround()
    {
        yield return new WaitForSeconds(5f);
        flowerSpriteRenderer.sprite = Resources.Load<Sprite>(cropItem.imgUrl);
        waterSpriteRenderer.sprite = Resources.Load<Sprite>("empty");
        step = STEP_READY_FLOWER;
    }

    private void FixedUpdate()
    {
        if (step != STEP_GROWS)
        {
            if (Vector2.Distance(this.transform.position, player.transform.position) < 2f)
            {
                readyForAction = true;
                cropSpriteRenderer.sprite = Resources.Load<Sprite>("cropSelected");
            }
            else
            {
                readyForAction = false;
                cropSpriteRenderer.sprite = Resources.Load<Sprite>("crop");
            }
        }
        else
        {
            cropSpriteRenderer.sprite = Resources.Load<Sprite>("crop");
        }
    }
}