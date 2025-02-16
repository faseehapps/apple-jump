using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AppleScript : MonoBehaviour
{
    public Animator animator;
    public float selfDestructPosition; // Relative to the main camera.
    public float movespeed;
    private bool hasGivenReward;
    private int moveDirection;

    private void Start()
    {
        hasGivenReward = false;
        if (gameObject.transform.position.x > 0)
        {
            moveDirection = -1;
        }
        else if (gameObject.transform.position.x < 0)
        {
            moveDirection = 1;
        }
    }

    void Update()
    {
        // To destroy the apple when it reaches a certain position.
        float cameraYPos = Camera.main.transform.position.y;
        if (gameObject.transform.position.y < cameraYPos + selfDestructPosition)
        {
            Destroy(gameObject, 0);
        }

        // To move the apple if player is level 3 or more.
        if (GameManagerScript.instance.levelNo >= 3)
        {
            if (gameObject.transform.position.x < 0)
            {
                float newXPosition = gameObject.transform.position.x + (movespeed * moveDirection * Time.deltaTime);
                gameObject.transform.position = new Vector3(
                    newXPosition,
                    gameObject.transform.position.y,
                    gameObject.transform.position.z
                );
            }
            if (gameObject.transform.position.x > 0)
            {
                float newXPosition = gameObject.transform.position.x + (movespeed * moveDirection * Time.deltaTime);
                gameObject.transform.position = new Vector3(
                    newXPosition,
                    gameObject.transform.position.y,
                    gameObject.transform.position.z
                );
            }
        }
    }


    // Functions //

    public void GiveReward()
    {
        if (!hasGivenReward)
        {
            GameManagerScript.instance.bonusScore += 10;
            animator.SetBool("hasGivenReward", true);
            AudioManager.instance.PlaySFX(AudioManager.instance.crunch);
            hasGivenReward = true;
        }
    }
}
