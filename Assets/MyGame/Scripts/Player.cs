using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    public AudioMixer mixer;

    public float timeUntilFrozen;
    public float torchDistanceThreshold;
    public PostProcessProfile postProcessProfile;
    public GameObject torch;

    private float currentTime;
    private bool torchIsGrabbed = false;

    public static bool chessIsActivated = false;

    private void Start()
    {
        postProcessProfile.GetSetting<ChromaticAberration>().intensity.value = 1;
        currentTime = 60;
        chessIsActivated = false;
    }

    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, torch.transform.position) < torchDistanceThreshold && !torchIsGrabbed)
        {
            torchIsGrabbed = true;
            postProcessProfile.GetSetting<ChromaticAberration>().intensity.value = 0;
            UserInterfaceManager.instance.timer.gameObject.SetActive(false);
        } else if(Vector3.Distance(gameObject.transform.position, torch.transform.position) > torchDistanceThreshold && torchIsGrabbed)
        {
            if (chessIsActivated) return;

            torchIsGrabbed = false;
            postProcessProfile.GetSetting<ChromaticAberration>().intensity.value = 1;
            UserInterfaceManager.instance.timer.gameObject.SetActive(true);
            currentTime = 60;
        }

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
            UserInterfaceManager.instance.DisplayRestartPanel("You froze to death!");
        }

        UserInterfaceManager.instance.SetTimer(currentTime, "Grab the torch before you freeze to death - ", color);
    }
}
