using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float rotationAgility;
    [SerializeField]
    private float standardForwardSpeed;
    [SerializeField]
    private float standardBackwardSpeed;
    [SerializeField]
    private float speedBoostTime;
    [SerializeField]
    private float speedBoostMultiplier;
    [SerializeField]
    private SpriteRenderer presentRenderer;

    private Rigidbody2D rb;
    private float currentForwardSpeed;
    private float currentBackwardSpeed;
    private bool carryingPresent = false;
    private bool isBoosted = false;

    public static int presentCount { get; private set; } = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentForwardSpeed = standardForwardSpeed;
        currentBackwardSpeed = standardBackwardSpeed;
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Vertical");
        float y = -Input.GetAxis("Horizontal");

        transform.Rotate(0, 0, y * rotationAgility);
        float speedFactor = x > 0 ? currentForwardSpeed : currentBackwardSpeed;

        Vector2 absVelocity = new Vector2(x, 0) * speedFactor;
        rb.velocity = transform.rotation * absVelocity;
    }

    private void CollectPresent(Sprite present)
    {
        Debug.Log(present);

        if (carryingPresent) return;
        presentRenderer.sprite = present;
        

        carryingPresent = true;
    }

    private void PlacePresent()
    {
        if (!carryingPresent) return;
        presentRenderer.sprite = null;

        carryingPresent = false;
        presentCount++;
    }

    private IEnumerator SpeedBoost()
    {
        if (isBoosted) yield break;
        isBoosted = true;

        YieldInstruction instruction = new WaitForEndOfFrame();
        float time = speedBoostTime;

        currentForwardSpeed *= speedBoostMultiplier;
        currentForwardSpeed *= speedBoostMultiplier;

        while (true)
        {
            time -= Time.deltaTime;

            if(time < 0)
            {
                currentForwardSpeed = standardForwardSpeed;
                currentBackwardSpeed = standardBackwardSpeed;

                isBoosted = false;
                break;
            }

            yield return instruction;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Present")
        {
            if (carryingPresent) return;

            Sprite sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            CollectPresent(sprite);
            Destroy(collision.gameObject);
        } else if(collision.gameObject.tag == "Spike")
        {
            SceneManager.LoadScene("LostScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Booster":
                StartCoroutine(SpeedBoost());
                break;
            case "PresentStore":
                PlacePresent();
                break;
        }
    }
}
