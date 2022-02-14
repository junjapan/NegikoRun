using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public Text highScoreText;

    public void Start()
    {
        highScoreText.text = "High Score:" + PlayerPrefs.GetInt("HighScore") + "m";
    }
    public void onStartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
    public void onClearScoreButtonClicked()
    {
        PlayerPrefs.SetInt("HighScore", 0);

        highScoreText.text = "High Score:" + PlayerPrefs.GetInt("HighScore") + "m";
    }
}
