using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Chess;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject boardContainer;
    public GameObject key;
    public Transform keySpawnPos;

    private void Start()
    {
        Board.puzzleFinishedDelegate += PuzzleFinished;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //boardContainer.SetActive(true);
            PuzzleFinished();
        }
    }

    private void PuzzleFinished()
    {
        UserInterfaceManager.instance.SetToNormalHand();
        UserInterfaceManager.instance.timer.gameObject.SetActive(false);
        Instantiate(key, keySpawnPos.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        Board.puzzleFinishedDelegate -= PuzzleFinished;
    }
}
