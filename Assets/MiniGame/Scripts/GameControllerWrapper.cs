//using UnityEngine;

//public class GameControllerWrapper : MonoBehaviour
//{
//    [SerializeField] private MiniGameController miniGameController;
//    [SerializeField] private GameManager gameManager;

//    void Start()
//    {
//        if (miniGameController == null) Debug.LogError("MiniGameController не назначен в GameControllerWrapper!");
//        if (gameManager == null) Debug.LogError("GameManager не назначен в GameControllerWrapper!");
//    }

//    public void StartMiniGame()
//    {
//        Debug.Log("GameControllerWrapper: StartMiniGame вызван");
//        miniGameController.StartMiniGame();
//        if (gameManager != null)
//        {
//            gameManager.StartGame();
//        }
//    }

//    public void ResetGame()
//    {
//        Debug.Log("GameControllerWrapper: ResetGame вызван");
//        miniGameController.ResetGame();
//        if (gameManager != null)
//        {
//            gameManager.EndGame();
//        }
//    }
//}