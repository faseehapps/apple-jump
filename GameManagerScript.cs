using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public GameObject player;
    public Text scoreText;
    public Text heightText;
    public GameObject levelScreen;
    public Text heightReached;
    public Text levelName;
    public GameObject gameOverScreen;

    // Global variables
    public bool gameOver;
    public int playerHeight; // How high the player is.
    public int playerScore;
    public int bonusScore; // WIll be added to playerScore.
    public int levelNo;

    private float levelScreenStartTime; // To show LevelScreen for a certain amount of time.
    private void Awake()
    {
        // Singleton pattern for GameManager.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameOver = false;
        playerHeight = 0;
        playerScore = 0;
        bonusScore = 0;
    }

    void Update()
    {
        // To check if the game is over.
        if (gameOver)
        {
            activateGameOverScreen();
        }

        // Height calculation.
        float height = (player.transform.position.y - 3.2f) * 2.5f;
        if (height < 0) // To prevent negative numbers.
        {
            height = 0;
        }
        if (height > playerHeight)
        {
            playerHeight = (int)height;
        }
        heightText.text = playerHeight.ToString();

        // Score calculation.
        float score = (playerHeight * 1.2f) + bonusScore;
        if (score < 0f) // To prevent negative numbers.
        {
            playerScore = 0;
        }
        if (score > playerScore)
        {
            playerScore = (int)score;
        }
        scoreText.text = (playerScore).ToString();

        // Level Calculation.

        // Level 2
        if (playerHeight >= 100 && levelNo < 2)
        {
            levelNo = 2;
            heightReached.text = "HeightReached: 100";
            levelName.text = "Lv2 - Mortal Apples!";
            levelScreenStartTime = Time.time;
            levelScreen.SetActive(true);
        }

        // Level 3
        else if (playerHeight >= 200 && levelNo < 3)
        {
            levelNo = 3;
            heightReached.text = "HeightReached: 200";
            levelName.text = "Lv3 - Ghost Apples!";
            levelScreenStartTime = Time.time;
            levelScreen.SetActive(true);
        }

        // To hide Level Screen.
        float levelScreenElapsedTime = Time.time - levelScreenStartTime;
        if (levelScreenElapsedTime > 3)
        {
            levelScreen.SetActive(false);
        }
    }



    // Functions //

    public void restartGame()
    {
        gameOverScreen.SetActive(false);
        gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void activateGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
}
