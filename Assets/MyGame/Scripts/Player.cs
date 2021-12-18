using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Player : MonoBehaviour
{
    public float timeUntilFrozen;
    public PostProcessProfile postProcessProfile;

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
        postProcessProfile.RemoveSettings<DepthOfField>();
        postProcessProfile.RemoveSettings<ChromaticAberration>();
    }

    private void PlayerWon()
    {

    }

    private void PlayerLost()
    {
        Debug.Log("You have lost");
    }
}
