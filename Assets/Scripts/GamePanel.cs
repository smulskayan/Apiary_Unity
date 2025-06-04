using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    private Button buttonClose;
    private Text caughtCountText;
    private Text allCountText;

    void Start()
    {
        buttonClose = GetComponentsInChildren<Button>()[0];
        if (buttonClose != null) {
            buttonClose.onClick.AddListener(closeGamePanel);
        }
        allCountText = GetComponentsInChildren<Text>()[0];
        caughtCountText = GetComponentsInChildren<Text>()[1];

        string extraTextAll, extraTextMy;
        int allWorm = MiniAppCrop.allCount;
        int myWorm = MiniAppCrop.caughtCount;

        if(allWorm == 1) { extraTextAll = " червь"; }
        else if (allWorm > 1 && allWorm < 5) { extraTextAll = " червя"; }
        else { extraTextAll = " червей"; }

        if(myWorm == 1) { extraTextMy = " червь"; }
        else if (myWorm > 1 && myWorm < 5) { extraTextMy = " червя"; }
        else { extraTextMy = " червей"; }
        
        allCountText.text = allWorm + extraTextAll;
        caughtCountText.text =  myWorm + extraTextMy;
    }

    void Update()
    {

    }

    public void closeGamePanel()
    {
        Destroy(gameObject);
    }
}
