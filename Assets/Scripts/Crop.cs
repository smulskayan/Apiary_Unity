using UnityEngine;
﻿using System.Collections;
using System.Collections.Generic;

public class Crop : MonoBehaviour
{
    private int STEP_EMPTY = 0;
    private int STEP_GROWS = 1;
    private int STEP_READY_FLOWER = 2;
    private int STEP_READY_NECTAR = 3;
    private int STEP_PLOW = 4;

    private SpriteRenderer seedSpriteRenderer;
    private SpriteRenderer flowerSpriteRenderer;
    private SpriteRenderer nectarSpriteRenderer;
    private SpriteRenderer cropSpriteRenderer;

    private Item cropItem;
    private int step = 0;

    private GameObject player;
    private bool readyForAction;

    void Start() {
        cropSpriteRenderer = GetComponent<SpriteRenderer>();
        seedSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[2];
        flowerSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[1];
        nectarSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[3];

        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPlaceCrop();
        }
    }

    void TryPlaceCrop() {
        Item item = new Item("flower", "flower", 1, Item.TYPEPFOOD, 10, 1, 5f);

        if (readyForAction)
        {
            if (step == STEP_EMPTY) 
            {
                if (item.type == Item.TYPEPFOOD) {
                    step = STEP_GROWS;
                    cropItem = item;
                    seedSpriteRenderer.sprite = Resources.Load<Sprite>("seeds");
                    StartCoroutine(grow());
                }
            }
            else if (step == STEP_READY_FLOWER)
            {
                step = STEP_GROWS;
                StartCoroutine(createNectar());
            }
            else if (step == STEP_READY_NECTAR) {
                nectarSpriteRenderer.sprite = Resources.Load<Sprite>("empty");
            }   
        }
    }

    private IEnumerator grow()
    {
        yield return new WaitForSeconds(cropItem.timeToGrow);
        seedSpriteRenderer.sprite = Resources.Load<Sprite>("empty");
        flowerSpriteRenderer.sprite = Resources.Load<Sprite>(cropItem.imgUrl);

        step = STEP_READY_FLOWER;
        // OnStepReady();
    }

    private IEnumerator createNectar()
    {
        yield return new WaitForSeconds(10f);
        nectarSpriteRenderer.sprite = Resources.Load<Sprite>("nectar");
        step = STEP_READY_NECTAR;
        // OnStepReady();
    }

    private void FixedUpdate()
    {
        Crop[] allCrops = FindObjectsOfType<Crop>();
        foreach (Crop crop in allCrops)
        {
            if (step != STEP_GROWS)
            {
                Debug.Log("Player position: " + player.transform.position);
                Debug.Log("Crop position: " + this.transform.position);
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

}
