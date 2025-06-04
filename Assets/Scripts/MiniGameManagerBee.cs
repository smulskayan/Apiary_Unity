using UnityEngine;

public class MiniGameManagerBee : MonoBehaviour
{
    public static MiniGameManagerBee Instance;

    public bool isGameStarted = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartGame()
    {
        isGameStarted = true;
    }
}
