using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Chess;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject boardContainer;
    public GameObject key;

    private void Start()
    {
        Board.puzzleFinishedDelegate += PuzzleFinished;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            boardContainer.SetActive(true);
        }
    }

    private void PuzzleFinished()
    {
        Instantiate(key, transform.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        Board.puzzleFinishedDelegate -= PuzzleFinished;
    }
}
