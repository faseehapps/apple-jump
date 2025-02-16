using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float jumpStrength;
    public float jumpStoppingVelocity; // when the Player toutches the apple to decrease the speed.
    public float gameOverVelocity;

    [Header("------ Debug -----")]
    public bool flymode;
    public float flyspeed;


    // To make the Player jump.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // To make the player jump.
        if (!GameManagerScript.instance.gameOver)
        {
            if (rb.velocity.y >= 0) // Jump Stop.
            {
                rb.velocity = new Vector2(0.0f, jumpStoppingVelocity);
                animator.SetBool("hasJumped", true);
                AudioManager.instance.PlaySFX(AudioManager.instance.stop);

                // To make the player recieve reward.
                AppleScript appleScript = collision.GetComponent<AppleScript>();
                if (appleScript != null)
                {
                    appleScript.GiveReward();
                }
            }
            else // Jump.
            {
                rb.velocity = Vector2.up * jumpStrength;
                animator.SetBool("hasJumped", true);
                AudioManager.instance.PlaySFX(AudioManager.instance.jump);

                // To destruct apple if the player is level 2 or more.
                if (GameManagerScript.instance.levelNo >= 2)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    void Update()
    {
        // To check if the game is over.
        if (!GameManagerScript.instance.gameOver)
        {
            if (rb.velocity.y < gameOverVelocity)
            {
                GameManagerScript.instance.gameOver = true;
                Debug.Log("Game Over!");
                AudioManager.instance.PlaySFX(AudioManager.instance.death);
            }
        }

        // To change the rb.position.x to mouse position in the x axis.
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        rb.position = new Vector2(worldMousePos.x, rb.position.y);

        // Flymode.
        if (flymode)
        {
            rb.velocity = new Vector2(0f, flyspeed);
        }
    }
}
