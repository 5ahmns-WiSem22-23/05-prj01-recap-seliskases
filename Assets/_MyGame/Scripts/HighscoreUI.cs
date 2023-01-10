using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highscoreTextField;

    private void Start()
    {
        float highscore = PlayerPrefs.GetFloat("highscore");
        float minutes = Mathf.FloorToInt(highscore / 60f);
        float seconds = Mathf.FloorToInt(highscore - minutes * 60);
        string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
        highscoreTextField.text = "Best time: " + timeString;
    }
}
