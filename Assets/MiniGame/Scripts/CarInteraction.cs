//using UnityEngine;
//using UnityEngine.UI;


//public class CarInteraction : MonoBehaviour
//{
//    [SerializeField] private float interactionDistance = 3f; // Дистанция для взаимодействия
//    [SerializeField] private GameObject interactButton; // Ссылка на кнопку UI
//    [SerializeField] private GameObject miniGameCanvas; // Ссылка на Canvas мини-игры
//    [SerializeField] private AudioClip openUISound; // Звук открытия UI
//    [SerializeField] private AudioClip closeUISound; // Звук закрытия UI
//    private Transform player; // Ссылка на трансформ игрока
//    private AudioSource audioSource; // Компонент AudioSource
//    private MiniGameController miniGameController; // Ссылка на контроллер мини-игры
//    private bool isPlayerNearby = false;

//    void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player")?.transform;
//        if (player == null)
//        {
//            Debug.LogError("Игрок с тегом 'Player' не найден!");
//        }

//        audioSource = GetComponent<AudioSource>();
//        if (audioSource == null)
//        {
//            Debug.LogError("AudioSource не найден на объекте " + gameObject.name);
//        }

//        if (miniGameCanvas != null)
//        {
//            miniGameController = miniGameCanvas.GetComponentInChildren<MiniGameController>(true);
//            if (miniGameController == null)
//            {
//                Debug.LogError("MiniGameController не найден в MiniGameCanvas или его дочерних объектах!");
//            }
//        }
//        else
//        {
//            Debug.LogError("MiniGameCanvas не назначен в инспекторе!");
//        }

//        if (interactButton != null)
//        {
//            interactButton.SetActive(false);
//        }
//        else
//        {
//            Debug.LogError("InteractButton не назначен в инспекторе!");
//        }

//        Debug.Log("CarInteraction: MiniGameCanvas начальное состояние: " + (miniGameCanvas != null ? miniGameCanvas.activeSelf.ToString() : "null"));
//    }

//    void Update()
//    {
//        if (player == null || miniGameCanvas == null || interactButton == null) return;

//        float distance = Vector3.Distance(player.position, transform.position);
//        isPlayerNearby = distance <= interactionDistance;

//        interactButton.SetActive(isPlayerNearby && !miniGameCanvas.activeSelf);
//    }

//    public void OpenMiniGameUI()
//    {
//        if (miniGameCanvas == null || miniGameController == null)
//        {
//            Debug.LogError("MiniGameCanvas или MiniGameController не назначены, невозможно открыть мини-игру!");
//            return;
//        }

//        miniGameCanvas.SetActive(true);
//        miniGameController.StartMiniGame();
//        if (interactButton != null)
//        {
//            interactButton.SetActive(false);
//        }
//        if (openUISound != null && audioSource != null)
//        {
//            audioSource.PlayOneShot(openUISound);
//        }
//        Debug.Log("CarInteraction: MiniGameCanvas открыт");
//    }

//    public void CloseMiniGameUI()
//    {
//        if (miniGameCanvas == null || miniGameController == null) return;

//        miniGameController.ResetGame();
//        miniGameCanvas.SetActive(false);
//        if (isPlayerNearby && interactButton != null)
//        {
//            interactButton.SetActive(true);
//        }
//        if (closeUISound != null && audioSource != null)
//        {
//            audioSource.PlayOneShot(closeUISound);
//        }
//        Debug.Log("CarInteraction: MiniGameCanvas закрыт");
//    }
//}

