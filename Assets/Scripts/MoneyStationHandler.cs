using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStationHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        {
            foreach (var item in PileHandler.instance.itemList)
            {
                Destroy(item);
                GameManager.actualMoney++;
            }
            PileHandler.instance.itemList.Clear();
            PileHandler.instance.itemListIndexCounter = 0;
        }
    }
}
