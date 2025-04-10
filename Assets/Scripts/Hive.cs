using UnityEngine;

public class Hive : MonoBehaviour
{
    private SpriteRenderer seedSpriteRenderer;
    private SpriteRenderer productSpriteRenderer;
    private SpriteRenderer cropSpriteRenderer;

    private Item cropItem;

    void Start() {
        cropSpriteRenderer = GetComponent<SpriteRenderer>();
        productSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[1];

        productSpriteRenderer.sprite = Resources.Load<Sprite>("honey");
    }

    void Update()
    {

    }

    void OnMouseDown() {
        Item item = new Item("honey", "bee", 1, Item.TYPEPFOOD, 10, 1);

        if (item.type == Item.TYPEPFOOD) {

            cropItem = item;
            seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/seeds");
        }
    }
}
