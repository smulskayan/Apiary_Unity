using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject shopPrefab;
    public bool shopOpened;
    private GameObject shop;

    public GameObject miniAppPanel;
    public bool panelOpened;
    private GameObject panel;

    public GameObject endPanel;

    public void openShop()
    {
        if (!shopOpened)
        {
            shop = Instantiate(shopPrefab);
            shop.transform.SetParent(gameObject.transform);
            shop.GetComponent<RectTransform>().offsetMin = new Vector2(100, 59);
            shop.GetComponent<RectTransform>().offsetMax = new Vector2(-100, -50);
            shop.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            shopOpened = true;
        }
    }
    public void closeShop()
    {
        if (shopOpened)
        {
            Destroy(shop);
            shopOpened = false;
        }
    }

    public void openPanel()
    {
        if (!panelOpened)
        {
            panel = Instantiate(miniAppPanel);
            panel.transform.SetParent(gameObject.transform);
            panel.GetComponent<RectTransform>().offsetMin = new Vector2(100, 59);
            panel.GetComponent<RectTransform>().offsetMax = new Vector2(-100, -50);
            panel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            panelOpened = true;
        }
    }
    public void closePanel()
    {
        if (panelOpened)
        {
            Destroy(panel);
            panelOpened = false;
            openEndPanel();
        }
    }

    public void openEndPanel()
    {
        endPanel.SetActive(true);
    }
}
