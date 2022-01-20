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

    private void Awake()
    {
        restartPanel.SetActive(false);
        timer.gameObject.SetActive(true);
    }

    public void DisplayRestartPanel(string msg)
    {
        timer.gameObject.SetActive(false);
        restartPanel.SetActive(true);
        message.text = msg;
    }

    public void SetTimer(float time, Color color)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timer.color = color;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
