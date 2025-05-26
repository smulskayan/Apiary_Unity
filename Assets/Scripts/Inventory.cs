using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    private List<Slot_build> slots = new List<Slot_build>();
    private RectTransform barRectTransform;

    void Update() 
    {
        slots = GetComponentsInChildren<Slot_build>().ToList();
        int i = 0;
        foreach (Slot_build slot in slots)
        {
            slot.fillSlot(i);
            i++;
        }
    }
}