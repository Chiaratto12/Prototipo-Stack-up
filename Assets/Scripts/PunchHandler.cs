using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchHandler : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] public PlayerController controller;
    [SerializeField] public PileHandler pileHandler;

    private void OnTriggerEnter(Collider other)
    {
        if(controller.animator.GetBool("Punch"))
        {
            Debug.Log("Bateu");
            pileHandler.AddToPile(other.gameObject);
        }
    }
}
