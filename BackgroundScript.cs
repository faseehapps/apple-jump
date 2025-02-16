using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public GameObject cameraObject;
    public Camera cameraComponent;
    public SpriteRenderer spriteRenderer;
    public float parallaxIntensity;

    private Vector3 initialPosition;
    private bool parallaxFull;
    private float halfHeight;
    void Start()
    {
        initialPosition = transform.position;
        parallaxFull = false; // True when the background reaches the maximum.
        halfHeight = spriteRenderer.bounds.size.y / 2; // Half height of the sprite renderer image.
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!parallaxFull)
        {
            // Parallax effect on background.
            gameObject.transform.position = new Vector3(
                cameraObject.transform.position.x, initialPosition.y + (cameraObject.transform.position.y * parallaxIntensity), 0f
            );

            // To check if parallax is full.
            float topmostYCoordinate = gameObject.transform.position.y + halfHeight;
            float topmostScreenYCoordinate = WorldYToScreenY(topmostYCoordinate);
            if (Screen.height > topmostScreenYCoordinate)
            {
                parallaxFull = true;
                Debug.Log("PositionFull");
            }
        }

        if (parallaxFull)
        {
            float cameraTopY = Camera.main.transform.position.y + Camera.main.orthographicSize;
            Vector3 newPosition = new Vector3(0f, cameraTopY - halfHeight, 0f);
            gameObject.transform.position = newPosition;
        }
    }

    float WorldYToScreenY(float worldYCoordinate)
    {
        Vector3 ScreenCoordinates = cameraComponent.WorldToScreenPoint(new Vector3(0f, worldYCoordinate, 0f));
        return ScreenCoordinates.y;
    }
}
