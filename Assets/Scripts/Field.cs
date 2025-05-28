using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    public GameObject miniAppPanel;
    public Button button;
    private GameObject panel;
    private GameObject player;
    private GameObject field;
    private bool allowClick = false;


    void Start() {
        player = GameObject.FindWithTag("Player");
        field = GameObject.FindWithTag("Field");
    }

    private void FixedUpdate() {
        if (Vector2.Distance(this.transform.position, player.transform.position) < 5f) {
            allowClick = true;
            button.gameObject.SetActive(true);
            button.onClick.AddListener(openPanel);
        }    
        else {
            allowClick = false;
            button.gameObject.SetActive(false);
        }
    }

    void openPanel()
    {
        if(miniAppPanel != null) {
            miniAppPanel.SetActive(true);
        }
    }
    public void closePanel()
    {
        if(miniAppPanel != null) {
            miniAppPanel.SetActive(false);
        }
    }
}