using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float startTime;

    public float currentTime { get; private set; }

    private void Update()
    {
        startTime -= Time.deltaTime;
    }
}
