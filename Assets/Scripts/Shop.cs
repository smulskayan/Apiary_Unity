using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameObject player;
    private GameObject inventory;
    private SpriteRenderer stallSpriteRenderer;
    private bool allowClick = false;

    // Start is called before the first frame update
    void Start() {
        stallSpriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        inventory = GameObject.FindWithTag("Inventory");
    }

    // Update is called once per frame
    void Update() {

    }

    void OnMouseDown() {
        if (allowClick) {
            // inventory.GetComponent<Menu>().openShop();
        }
    }

    private void FixedUpdate() {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 0.2f) {
            allowClick = true;
            stallSpriteRenderer.sprite = Resources.Load<Sprite>("Tools/Shop");
        }
        else {
            allowClick = false;
            stallSpriteRenderer.sprite = Resources.Load<Sprite>("Tools/ShopEmpty");
        }
    }
}