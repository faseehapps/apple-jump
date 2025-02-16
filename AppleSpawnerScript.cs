using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawnerScript : MonoBehaviour
{
    public GameObject apple;
    public int appleSpawnFrequency;

    private float nextAppleYPosition;

    void Start()
    {
        nextAppleYPosition = 2;
    }

    void Update()
    {
        // To make the spawner be above the camera all time:
        Vector3 newPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 1.4f, gameObject.transform.position.z);
        gameObject.transform.position = newPosition;

        // To spawn apples:
        if (gameObject.transform.position.y >= nextAppleYPosition)
        {
            SpawnApple();
            nextAppleYPosition += appleSpawnFrequency;
        }
    }

    void SpawnApple()
    {
        Vector3 applePosition = new Vector3(Random.Range(-2.0f, 2.0f), transform.position.y, transform.position.z);
        Instantiate(apple, applePosition, transform.rotation);
    }
}
