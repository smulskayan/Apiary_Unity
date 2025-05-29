using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HoneyCatchMinigame : MonoBehaviour
{
    public GameObject minigamePanel;
    public GameObject barrel;
    public GameObject honeyPrefab;
    public GameObject trashPrefab;
    public Transform spawnTop;
    public TextMeshProUGUI honeyCounterText;
    public float fallSpeed = 3f;
    public float spawnInterval = 1f;
    public int honeyToCatch = 10;
    private int honeyCaught = 0;
    private int honeyMissed = 0;
    private bool isGameActive = false;
    private XPManager xpManager;

    void Start()
    {
        minigamePanel.SetActive(false);
        xpManager = FindObjectOfType<XPManager>();
    }

    public void StartMinigame()
    {
        if (!isGameActive)
        {
            minigamePanel.SetActive(true);
            honeyCaught = 0;
            honeyMissed = 0;
            isGameActive = true;
            UpdateHoneyCounter();
            StartCoroutine(SpawnObjects());
        }
    }

    IEnumerator SpawnObjects()
    {
        while (isGameActive)
        {
            GameObject prefab = Random.value > 0.3f ? honeyPrefab : trashPrefab;
            Vector3 spawnPos = new Vector3(Random.Range(-4f, 4f), spawnTop.position.y, 0);
            GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity, minigamePanel.transform);
            obj.AddComponent<FallingObject>().Init(this, fallSpeed);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void CatchHoney()
    {
        honeyCaught++;
        UpdateHoneyCounter();
        if (honeyCaught >= honeyToCatch)
        {
            EndGame(true);
        }
    }

    public void MissHoney()
    {
        honeyMissed++;
        if (honeyMissed >= honeyToCatch)
        {
            EndGame(false);
        }
    }

    void UpdateHoneyCounter()
    {
        honeyCounterText.text = $"Honey: {honeyCaught}/{honeyToCatch}";
    }

    void EndGame(bool success)
    {
        isGameActive = false;
        StopAllCoroutines();
        foreach (Transform child in minigamePanel.transform)
        {
            if (child.CompareTag("Honey") || child.CompareTag("Trash"))
                Destroy(child.gameObject);
        }
        if (success && xpManager != null)
        {
            xpManager.AddXP(1);
        }
        minigamePanel.SetActive(false);
    }
}