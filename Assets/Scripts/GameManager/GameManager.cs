using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{


  [Header("Game Manager Variables")]
  [SerializeField] float timeLeft = 60f; // Time left in seconds
  [SerializeField] float startTime = 5f;
  bool isGameOver = false; // Flag to check if the game is over

  [Header("Game Manager Dependencies")]
  [SerializeField] TMP_Text timeText;
  [SerializeField] GameObject gameOverText;
  [SerializeField] PlayerMovement playerMovement; // Reference to the player object

  public bool IsGameOver => isGameOver;

  private void Start()
  {
     timeLeft = startTime; // Initialize time left with start time
  }

  private void Update()
  {

    // Game Over condition
    if(timeLeft <= 0f)
    {
      GameOver();
    }
    else
    {
      DecreaseTime();

    }
  }

  public void AddTime(float timeToAdd)
  {
    // Adds time to the timer
    if (!isGameOver) // Only add time if the game is not over
    {
      timeLeft += timeToAdd;
      timeText.text = string.Format("Time Left: {0}", timeLeft.ToString("F1"));
    }
  }

  private void DecreaseTime()
  {
    // Update the timer every frame
    timeLeft -= Time.deltaTime;
    timeText.text = string.Format("Time Left: {0}", timeLeft.ToString("F1"));
  }

  private void GameOver()
  {
    playerMovement.enabled = false; // Disable player movement
    timeLeft = 0f; // Ensure time does not go negative
    gameOverText.SetActive(true); // Show Game Over text
    Time.timeScale = 0.1f; // Pause the game
    isGameOver = true; // Set game over flag
  }


  public bool GetGameOverState()
  {
    // Returns the game over state
    return isGameOver;
  }

  }
