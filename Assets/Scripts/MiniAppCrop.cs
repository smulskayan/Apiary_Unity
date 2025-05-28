using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MiniAppCrop : MonoBehaviour
{
    private Field field;
    private Button buttonClose;
    private Text moneyText;
    private Text caughtCountText;
    private Text allCountText;
    private int caughtCount = 0;
    private int allCount = 0;
    public Crop_with_hole crop;
    public Worm worm;

    public Crop_with_hole[,] crops = new Crop_with_hole[3, 3];
    public float timer = 0f;

    void CreateCrops() {
        for(int i = 0; i < crops.GetLength(0); i++) {
            for(int j = 0; j < crops.GetLength(1); j++) {
                Vector3 pos = new Vector3(-i*250 + 1200, j*220 + 300, 0);
                crops[i, j] = (Crop_with_hole)Instantiate(crop, pos, Quaternion.identity);
                crops[i, j].transform.SetParent(transform);
            }
        }
    }

    void CreateWorm() {
        int x = Random.Range(0, crops.GetLength(0)), y = Random.Range(0, crops.GetLength(0));
        Worm w = (Worm)Instantiate(worm, crops[x,y].transform.GetChild(0).position, Quaternion.identity);
        w.transform.SetParent(crops[x,y].transform);
    }

    void Start()
    {
        buttonClose = GetComponentsInChildren<Button>()[0];
        field = GameObject.FindWithTag("Field").GetComponent<Field>();

        moneyText = GetComponentsInChildren<Text>()[0];
        caughtCountText = GetComponentsInChildren<Text>()[1];
        allCountText = GetComponentsInChildren<Text>()[2];

        allCountText.text = allCount.ToString();
        caughtCountText.text = caughtCount.ToString();

        UpdateMoneyText();
        if (buttonClose != null) 
        {
            buttonClose.onClick.AddListener(field.closePanel);
        }
        CreateCrops();
        CreateWorm();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2) 
        {
            CreateWorm();
            allCountText.text = (++allCount).ToString();
            timer = 0;
        }
    }

    public void UpdateMoneyText()
    {
        if (moneyText != null) {
            moneyText.text = Player.money + "$";
        }
    }
    
    public void UpdateCaughtCountText()
    {
        if (caughtCountText != null) {
            caughtCountText.text = (++caughtCount).ToString();
        }
    }
}