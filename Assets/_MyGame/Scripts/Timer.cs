using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float startTime;
    [SerializeField]
    private TextMeshProUGUI textField;

    public float currentTime { get; private set; }

    private void Start()
    {
        currentTime = startTime;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        float minutes = Mathf.FloorToInt(currentTime / 60f);
        float seconds = Mathf.FloorToInt(currentTime - minutes * 60);
        string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
        textField.text = timeString;
    }
}
