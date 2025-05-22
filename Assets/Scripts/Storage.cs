using UnityEngine;

public class Storage : MonoBehaviour
{
    private int honeyAmount = 0;

    public void AddHoney(int amount)
    {
        honeyAmount += amount;
        Debug.Log($"Added {amount} honey, total: {honeyAmount}");
    }

    public int GetHoneyAmount()
    {
        Debug.Log($"GetHoneyAmount: {honeyAmount}");
        return honeyAmount;
    }
}