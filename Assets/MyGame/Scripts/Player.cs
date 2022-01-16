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

        float minutes = Mathf.Floor(currentTime / 60);
        float seconds = Mathf.RoundToInt(currentTime % 60);

        freezeCounter.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(currentTime > 40)
        {
            freezeCounter.color = Color.green;
        } else if(currentTime > 20)
        {
            freezeCounter.color = Color.yellow;
        } else if(currentTime > 0)
        {
            freezeCounter.color = Color.red;
        } else
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
}
