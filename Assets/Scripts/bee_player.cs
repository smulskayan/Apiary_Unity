using UnityEngine;
using TMPro;
using System.Collections;

public class bee_player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce = 5f;
    public int points = 0;
    public int maxPoints = 10;

    public Vector3 startPosition = new Vector3(0, -2f, 0);

    public TextMeshProUGUI coinCounterText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI loseText;

    public GameUIManager gameUIManager;

    private bool isDead = false;

    private GameObject[] coins;
    private Vector3[] coinsStartPositions;

    private GameObject[] obstacles;
    private Vector3[] obstaclesStartPositions;

    void Start()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.simulated = false;
        loseText?.gameObject.SetActive(false);
        points = 0;

        coins = GameObject.FindGameObjectsWithTag("Coin");
        coinsStartPositions = new Vector3[coins.Length];
        for (int i = 0; i < coins.Length; i++)
        {
            coinsStartPositions[i] = coins[i].transform.position;
        }

        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        obstaclesStartPositions = new Vector3[obstacles.Length];
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstaclesStartPositions[i] = obstacles[i].transform.position;
        }

        UpdateCoinUI();

        StartCoroutine(CountdownAndStart());
    }

    void Update()
    {
        if (MiniGameManagerBee.Instance == null || !MiniGameManagerBee.Instance.isGameStarted || isDead)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isDead) return;

        Die();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin") && !isDead && MiniGameManagerBee.Instance != null && MiniGameManagerBee.Instance.isGameStarted)
        {
            points++;
            other.gameObject.SetActive(false);
            UpdateCoinUI();

            Player.money += 10;

            if (points >= maxPoints)
            {
                WinGame();
            }
        }
    }

    void UpdateCoinUI()
    {
        if (coinCounterText != null)
            coinCounterText.text = points + " / " + maxPoints;
    }

    void WinGame()
    {
        isDead = true;
        rb.simulated = false;

        if (loseText != null)
        {
            loseText.gameObject.SetActive(true);
            loseText.text = $"Вы выиграли!\nПолучено монет: {points * 10}";
        }

        if (MiniGameManagerBee.Instance != null)
            MiniGameManagerBee.Instance.isGameStarted = false;

        StartCoroutine(ShowApiaryAfterDelay());
    }

    void Die()
    {
        isDead = true;
        rb.simulated = false;

        if (loseText != null)
        {
            loseText.gameObject.SetActive(true);
            loseText.text = $"Получено монет: {points * 10}";
        }

        if (MiniGameManagerBee.Instance != null)
            MiniGameManagerBee.Instance.isGameStarted = false;

        StartCoroutine(ShowApiaryAfterDelay());
    }

    IEnumerator CountdownAndStart()
    {
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);

            countdownText.text = "3";
            yield return new WaitForSeconds(1f);

            countdownText.text = "2";
            yield return new WaitForSeconds(1f);

            countdownText.text = "1";
            yield return new WaitForSeconds(1f);

            countdownText.text = "Старт!";
            yield return new WaitForSeconds(1f);

            countdownText.gameObject.SetActive(false);
        }

        rb.simulated = true;
        isDead = false;

        if (MiniGameManagerBee.Instance != null)
            MiniGameManagerBee.Instance.isGameStarted = true;
    }

    public void ResetMiniGame()
    {
        points = 0;
        isDead = false;

        rb.simulated = false;
        transform.position = startPosition;
        rb.linearVelocity = startPosition;

        loseText?.gameObject.SetActive(false);
        UpdateCoinUI();

        if (coins == null || coinsStartPositions == null)
        {
            coins = GameObject.FindGameObjectsWithTag("Coin");
            coinsStartPositions = new Vector3[coins.Length];
            for (int i = 0; i < coins.Length; i++)
            {
                coinsStartPositions[i] = coins[i].transform.position;
            }
        }

        if (obstacles == null || obstaclesStartPositions == null)
        {
            obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            obstaclesStartPositions = new Vector3[obstacles.Length];
            for (int i = 0; i < obstacles.Length; i++)
            {
                obstaclesStartPositions[i] = obstacles[i].transform.position;
            }
        }

        for (int i = 0; i < coins.Length; i++)
        {
            if (coins[i] != null) 
            {
                coins[i].transform.position = coinsStartPositions[i];
                coins[i].SetActive(true);
            }
        }

        for (int i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i] != null)
            {
                obstacles[i].transform.position = obstaclesStartPositions[i];
                obstacles[i].SetActive(true);
            }
        }

        StartCoroutine(CountdownAndStart());
    }
    IEnumerator ShowApiaryAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        gameUIManager?.ExitMiniGame();
        loseText?.gameObject.SetActive(false);
    }
}
