using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public GameObject apiaryRoot;
    public GameObject miniGameRoot;
    public Canvas miniGameCanvas;
    public bee_player beePlayerScript;

    public void StartMiniGame()
    {
        apiaryRoot.SetActive(false);
        miniGameRoot.SetActive(true);
        miniGameCanvas.gameObject.SetActive(true);

        if (beePlayerScript != null)
        {
            beePlayerScript.ResetMiniGame(); // перезапускаем мини-игру
        }
        else
        {
            Debug.LogWarning("bee_player script не назначен в GameUIManager!");
        }
    }

    public void ExitMiniGame()
    {
        miniGameRoot.SetActive(false);
        apiaryRoot.SetActive(true);
        miniGameCanvas.gameObject.SetActive(false);
    }
}
