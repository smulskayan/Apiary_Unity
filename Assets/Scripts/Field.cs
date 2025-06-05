using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    public Button button;
    private Menu inventory;
    private GameObject player;

    void Start() {
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Menu>();
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate() {
        if (Vector2.Distance(this.transform.position, player.transform.position) < 5f) {
            button.gameObject.SetActive(true);
            button.onClick.AddListener(inventory.openPanel);
        }    
        else {
            button.gameObject.SetActive(false);
        }
    }
}