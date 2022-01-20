using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager instance;

    public TextMeshProUGUI message;

    public void SetMessage(string msg)
    {
        message.text = msg;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
