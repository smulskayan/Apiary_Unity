using UnityEngine;

public class Stall : MonoBehaviour
{
    private GameObject player;
    private GameObject inventory;
    private SpriteRenderer stallSpriteRenderer;
    private bool allowClick = false;

    void Start() {
        stallSpriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        inventory = GameObject.FindWithTag("Inventory");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            TryPlaceShop();
        }
    }

    void TryPlaceShop() {
        if (allowClick)
        {
            inventory.GetComponent<Menu>().openShop();
        }
    }

    private void FixedUpdate() {
        if (Vector2.Distance(this.transform.position, player.transform.position) < 5f) {
            allowClick = true;
            stallSpriteRenderer.sprite = Resources.Load<Sprite>("ShopWithBear");
        }
        else {
            allowClick = false;
            stallSpriteRenderer.sprite = Resources.Load<Sprite>("ShopEmpty");
        }
    }
}