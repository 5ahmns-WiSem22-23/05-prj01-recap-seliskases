using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] presents;
    [SerializeField]
    private float spawnFrequency;
    [SerializeField]
    private float spawnRangeX;
    [SerializeField]
    private float spawnRangeY;

    private float timeSinceLastSpawn = 0f;

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn > spawnFrequency)
        {
            SpawnPresent();
            timeSinceLastSpawn = 0;
        }
    }

    private void SpawnPresent()
    {
        float x = Random.Range(transform.position.x - spawnRangeX / 2, transform.position.x + spawnRangeX / 2);
        float y = Random.Range(transform.position.y - spawnRangeY / 2, transform.position.y + spawnRangeY / 2);
        Vector2 spawnPos = new Vector2(x, y);

        int presentIndex = Random.Range(0, presents.Length);
        GameObject present = presents[presentIndex];

        Instantiate(present, spawnPos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(new Vector2(transform.position.x + spawnRangeX / 2, transform.position.y + spawnRangeY / 2), new Vector2(transform.position.x - spawnRangeX / 2, transform.position.y + spawnRangeY / 2));
        Gizmos.DrawLine(new Vector2(transform.position.x + spawnRangeX / 2, transform.position.y + spawnRangeY / 2), new Vector2(transform.position.x + spawnRangeX / 2, transform.position.y - spawnRangeY / 2));
        Gizmos.DrawLine(new Vector2(transform.position.x - spawnRangeX / 2, transform.position.y - spawnRangeY / 2), new Vector2(transform.position.x - spawnRangeX / 2, transform.position.y + spawnRangeY / 2));
        Gizmos.DrawLine(new Vector2(transform.position.x - spawnRangeX / 2, transform.position.y - spawnRangeY / 2), new Vector2(transform.position.x + spawnRangeX / 2, transform.position.y - spawnRangeY / 2));
    }
}
