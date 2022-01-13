using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Chess;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject boardContainer;

    private void Start()
    {
        boardContainer.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            boardContainer.SetActive(true);
            new Board();
        }
    }
}
