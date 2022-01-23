using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager instance;

    public TextMeshProUGUI message;
    public TextMeshProUGUI timer;
    public GameObject restartPanel;
    public GameObject leftHandNormal;
    public GameObject leftHandGaze;

    private void Awake()
    {
        instance = this;

        restartPanel.SetActive(false);
        timer.gameObject.SetActive(true);

        SetToNormalHand();
    }

    public void DisplayRestartPanel(string msg)
    {
        timer.gameObject.SetActive(false);
        restartPanel.SetActive(true);
        message.text = msg;

        SetToGazeHand();
    }

    public void SetTimer(float time, Color color)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timer.color = color;
    }

    public void SetToGazeHand()
    {
        leftHandGaze.SetActive(true);
        leftHandNormal.SetActive(false);
    }

    public void SetToNormalHand()
    {
        leftHandNormal.SetActive(true);
        leftHandGaze.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
