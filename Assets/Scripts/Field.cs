using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    public Button button;
    private Menu inventory;
    private GameObject player;
    private bool allowClick = false;

    void Start() {
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Menu>();
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate() {
        if (Vector2.Distance(this.transform.position, player.transform.position) < 5f) {
            allowClick = true;
            button.gameObject.SetActive(true);
            button.onClick.AddListener(inventory.openPanel);
        }    
        else {
            allowClick = false;
            button.gameObject.SetActive(false);
        }
    }
}