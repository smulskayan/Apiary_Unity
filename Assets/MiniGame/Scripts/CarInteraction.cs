//using UnityEngine;
//using UnityEngine.UI;


//public class CarInteraction : MonoBehaviour
//{
//    [SerializeField] private float interactionDistance = 3f; // ��������� ��� ��������������
//    [SerializeField] private GameObject interactButton; // ������ �� ������ UI
//    [SerializeField] private GameObject miniGameCanvas; // ������ �� Canvas ����-����
//    [SerializeField] private AudioClip openUISound; // ���� �������� UI
//    [SerializeField] private AudioClip closeUISound; // ���� �������� UI
//    private Transform player; // ������ �� ��������� ������
//    private AudioSource audioSource; // ��������� AudioSource
//    private MiniGameController miniGameController; // ������ �� ���������� ����-����
//    private bool isPlayerNearby = false;

//    void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player")?.transform;
//        if (player == null)
//        {
//            Debug.LogError("����� � ����� 'Player' �� ������!");
//        }

//        audioSource = GetComponent<AudioSource>();
//        if (audioSource == null)
//        {
//            Debug.LogError("AudioSource �� ������ �� ������� " + gameObject.name);
//        }

//        if (miniGameCanvas != null)
//        {
//            miniGameController = miniGameCanvas.GetComponentInChildren<MiniGameController>(true);
//            if (miniGameController == null)
//            {
//                Debug.LogError("MiniGameController �� ������ � MiniGameCanvas ��� ��� �������� ��������!");
//            }
//        }
//        else
//        {
//            Debug.LogError("MiniGameCanvas �� �������� � ����������!");
//        }

//        if (interactButton != null)
//        {
//            interactButton.SetActive(false);
//        }
//        else
//        {
//            Debug.LogError("InteractButton �� �������� � ����������!");
//        }

//        Debug.Log("CarInteraction: MiniGameCanvas ��������� ���������: " + (miniGameCanvas != null ? miniGameCanvas.activeSelf.ToString() : "null"));
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
//            Debug.LogError("MiniGameCanvas ��� MiniGameController �� ���������, ���������� ������� ����-����!");
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
//        Debug.Log("CarInteraction: MiniGameCanvas ������");
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
//        Debug.Log("CarInteraction: MiniGameCanvas ������");
//    }
//}

