using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float timeUntilFrozen;

    private float currentTime;
    private bool torchIsGrabbed = false;

    private void Update()
    {
        if (torchIsGrabbed) return;

        currentTime += Time.deltaTime;

        if(currentTime > timeUntilFrozen)
        {
            PlayerLost();
        }
    }

    public void OnTorchGrabbed()
    {
        Debug.Log("Torch is grabbed");
        torchIsGrabbed = true;
    }

    private void PlayerWon()
    {

    }

    private void PlayerLost()
    {
        Debug.Log("You have lost");
    }
}
