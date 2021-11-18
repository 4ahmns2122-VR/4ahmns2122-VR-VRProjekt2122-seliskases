using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Chess;

public class PuzzleManager : MonoBehaviour
{
    private void Start()
    {
        StartChess();
    }

    public void StartChess()
    {
        new Board();
    }
}
