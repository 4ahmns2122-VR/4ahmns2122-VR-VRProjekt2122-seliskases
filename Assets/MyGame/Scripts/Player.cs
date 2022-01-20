using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class Player : MonoBehaviour
{
    public float timeUntilFrozen;
    public PostProcessProfile postProcessProfile;
    public TextMeshProUGUI freezeCounter;
    

    private float currentTime;
    private bool torchIsGrabbed = false;

    private void Start()
    {
        postProcessProfile.GetSetting<ChromaticAberration>().intensity.value = 1;
        currentTime = 60;
    }

    private void Update()
    {
        if (torchIsGrabbed) return;

        currentTime -= Time.deltaTime;

        Color color = Color.white;

        if(currentTime > 40)
        {
            color = Color.green;
        } else if(currentTime > 20)
        {
            color = Color.yellow;
        } else if(currentTime > 0)
        {
            color = Color.red;
        } else
        {
            PlayerLost();
        }

        UserInterfaceManager.instance.SetTimer(currentTime, color);
    }

    public void OnTorchGrabbed()
    {
        Debug.Log("Torch is grabbed");
        torchIsGrabbed = true;
        postProcessProfile.GetSetting<ChromaticAberration>().intensity.value = 0;
    }

    private void PlayerLost()
    {
        UserInterfaceManager.instance.DisplayRestartPanel("You lost!");
    }
}
