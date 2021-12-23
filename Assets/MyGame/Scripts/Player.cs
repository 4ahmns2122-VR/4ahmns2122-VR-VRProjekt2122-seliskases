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

    private void Start()
    {
        postProcessProfile.GetSetting<ChromaticAberration>().intensity.value = 1;
    }

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
        postProcessProfile.GetSetting<ChromaticAberration>().intensity.value = 0;
    }

    private void PlayerWon()
    {
        // TODO: Implement PlayerWon Behaviour here
    }

    private void PlayerLost()
    {
        Debug.Log("You have lost");
    }

    public void Test()
    {
        postProcessProfile.GetSetting<Bloom>().intensity.value = 100;
    }
}
