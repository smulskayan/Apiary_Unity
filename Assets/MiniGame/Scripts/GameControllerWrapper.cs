//using UnityEngine;

//public class GameControllerWrapper : MonoBehaviour
//{
//    [SerializeField] private MiniGameController miniGameController;
//    [SerializeField] private GameManager gameManager;

//    void Start()
//    {
//        if (miniGameController == null) Debug.LogError("MiniGameController �� �������� � GameControllerWrapper!");
//        if (gameManager == null) Debug.LogError("GameManager �� �������� � GameControllerWrapper!");
//    }

//    public void StartMiniGame()
//    {
//        Debug.Log("GameControllerWrapper: StartMiniGame ������");
//        miniGameController.StartMiniGame();
//        if (gameManager != null)
//        {
//            gameManager.StartGame();
//        }
//    }

//    public void ResetGame()
//    {
//        Debug.Log("GameControllerWrapper: ResetGame ������");
//        miniGameController.ResetGame();
//        if (gameManager != null)
//        {
//            gameManager.EndGame();
//        }
//    }
//}