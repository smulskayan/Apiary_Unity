using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public GameObject shopPrefab;

    public bool inventoryOpened;
    public bool shopOpened;
    private GameObject inventory;
    private GameObject shop;

    public void openInventory()
    {
        if (shopOpened)
        {
            Destroy(shop);
            shopOpened = false;
        }
        else 
        {
            if (inventoryOpened)
            {
                Destroy(inventory);
                inventoryOpened = false;
            }
            else
            {
                inventory = Instantiate(inventoryPrefab);
                inventory.transform.SetParent(gameObject.transform);
                inventory.GetComponent<RectTransform>().offsetMin = new Vector2(100, 59);
                inventory.GetComponent<RectTransform>().offsetMax = new Vector2(-100, -50);
                inventory.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                inventoryOpened = true;
            }
        }
    }

    public void openShop()
    {
        if(!inventoryOpened && !shopOpened)
        {
            shop = Instantiate(shopPrefab);
            shop.transform.SetParent(gameObject.transform);
            shop.GetComponent<RectTransform>().offsetMin = new Vector2(100, 59);
            shop.GetComponent<RectTransform>().offsetMax = new Vector2(-100, -50);
            shop.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            shopOpened = true;
        }
    }
}
