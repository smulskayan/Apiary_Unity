using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_build : MonoBehaviour
{
    private Image slotImage;
    private Image itemImage;
    private Text countText;
    private int id;

    public void fillSlot(int id)
    {
        this.id = id;
        Item item = Player.items[id];

        slotImage = GetComponent<Image>();
        itemImage = GetComponentsInChildren<Image>()[1];
        countText = GetComponentInChildren<Text>();

        if (item.count > 0) countText.text = "x" + item.count.ToString();
        else countText.text = "";
        itemImage.sprite = Resources.Load<Sprite>(item.imgUrl);
    }
}