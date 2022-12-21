using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     Rigidbody2D rb;

    [SerializeField]
    private float rotationSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Vertical");
        float y = -Input.GetAxis("Horizontal");

        transform.Rotate(0, 0, y * rotationSpeed);
        rb.velocity = new Vector2(x, 0);
    }


}
