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

        if(allWorm == 1) { extraTextAll = " червяк"; }
        else if (allWorm > 1 && allWorm < 5) { extraTextAll = " червяка"; }
        else { extraTextAll = " червяков"; }

        if(myWorm == 1) { extraTextMy = " червяк"; }
        else if (myWorm > 1 && myWorm < 5) { extraTextMy = " червяка"; }
        else { extraTextMy = " червяков"; }
        
        allCountText.text = allWorm + extraTextAll;
        caughtCountText.text =  myWorm + extraTextMy;
    }

    public void closeGamePanel()
    {
        gameObject.SetActive(false);
    }
}
