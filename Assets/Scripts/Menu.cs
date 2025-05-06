using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject shopPrefab;
    public bool shopOpened;
    private GameObject shop;

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
}
