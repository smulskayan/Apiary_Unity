using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private List<ShopItem> allItems = new List<ShopItem>();
    private List<Item> allProductList = new List<Item>();
    private Text moneyText;
    private Button buttonClose;
    private Menu menu;
    private GameObject inventory;

    void Start()
    {
        buttonClose = GetComponentsInChildren<Button>()[0];
        moneyText = GetComponentsInChildren<Text>()[0];
        moneyText.text = Player.money + "$";
        inventory = GameObject.FindWithTag("Inventory");

        allItems = gameObject.GetComponentsInChildren<ShopItem>().ToList();

        fillAllProducts();
        StartCoroutine(fillShop());
    }

    void Update()
    {
        if (buttonClose != null) 
        {
            buttonClose.onClick.AddListener(inventory.GetComponent<Menu>().closeShop);
        }
    }

    private IEnumerator fillShop()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < allItems.Count; i++)
        {
            allItems[i].UpdateItem(allProductList[i], moneyText);
        }
    }

    private void fillAllProducts()
    {
        allProductList.Clear();
        allProductList.Add(new Item("nectar", "nectar", 1, Item.TYPEPFOOD, 10, 2, 60f));
        allProductList.Add(new Item("jarHoney", "jar_honey", 1, Item.TYPEPFOOD, 25, 1, 10f));
        allProductList.Add(new Item("seeds", "seeds", 1, Item.TYPEPFOOD, 10, 3, 10f));
        allProductList.Add(new Item("honey", "honey", 1, Item.TYPEPFOOD, 10, 4, 10f));
    }

}