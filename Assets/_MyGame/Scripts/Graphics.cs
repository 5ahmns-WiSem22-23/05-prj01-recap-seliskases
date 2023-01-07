using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphics : MonoBehaviour
{
    [SerializeField]
    private Material boosterMat;
    [SerializeField]
    private float sinSpeed = 1f;

    private void Update()
    {
        //Value must be between -2 and 2
        float curValue = Mathf.Sin(Time.time * sinSpeed) * 2;

        boosterMat.SetFloat("_Value", curValue);

    }
}
