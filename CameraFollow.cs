using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    public Vector3 yOffset; // Offset distance from the game object which the camera follows.
    public float smoothingSpeed;

    void FixedUpdate()
    {
        // Smooth camera follow
        if (objectToFollow != null)
        {
            Vector3 currentPosition = transform.position;
            Vector3 newPosition = currentPosition;
            if (!GameManagerScript.instance.gameOver)
            {
                newPosition = new Vector3(currentPosition.x, objectToFollow.transform.position.y + yOffset.y, currentPosition.z);
            }
            Vector3 newPositionSmoothed = Vector3.Lerp(currentPosition, newPosition, smoothingSpeed);
            transform.position = newPositionSmoothed;
        }
    }
}
