using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float agility;
    [SerializeField]
    private float zoomOutFactor;

    void FixedUpdate()
    {
        float t = agility * Time.deltaTime;

        Vector3 position = transform.position;
        position.y = Mathf.Lerp(transform.position.y, player.transform.position.y, t);
        position.x = Mathf.Lerp(transform.position.x, player.transform.position.x, t);

        transform.position = position;

        float distToPlayer = Vector3.Distance(player.transform.position, transform.position) + transform.position.z;
        GetComponent<Camera>().orthographicSize = 5 + (distToPlayer * zoomOutFactor);
    }
}