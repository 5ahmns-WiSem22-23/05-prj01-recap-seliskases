using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private float startTime;
    [SerializeField]
    private int requiredCount;

    [SerializeField]
    private TextMeshProUGUI timeTextField;
    [SerializeField]
    private TextMeshProUGUI countTextField;

    private float currentTime;

    private void Start()
    {
        currentTime = startTime;

        float highscore = PlayerPrefs.GetFloat("highscore");
        if(highscore == 0)
        {
            PlayerPrefs.SetFloat("highscore", startTime);
        }
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        float minutes = Mathf.FloorToInt(currentTime / 60f);
        float seconds = Mathf.FloorToInt(currentTime - minutes * 60);
        string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
        timeTextField.text = timeString;

        string countString = PlayerController.presentCount + "/" + requiredCount + " presents collected";
        countTextField.text = countString;

        if(PlayerController.presentCount == requiredCount)
        {
            float highscore = PlayerPrefs.GetFloat("highscore");
            if(currentTime < highscore)
            {
                PlayerPrefs.SetFloat("highscore", currentTime);
            }
            SceneManager.LoadScene(2);
        }

        if(currentTime < 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}
