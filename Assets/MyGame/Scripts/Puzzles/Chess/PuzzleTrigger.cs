using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Chess;

public class PuzzleTrigger : MonoBehaviour
{
    public Board board;

    private void Start()
    {
        board.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            board.gameObject.SetActive(true);
            new Board();
        }
    }
}
