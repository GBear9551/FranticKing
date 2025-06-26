using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   [Header("Score Manager Settings")]  
   [SerializeField] TextMeshProUGUI scoreText;
   [SerializeField] int score = 0;

   [Header("Score Manager Dependencies")]
   [SerializeField] GameManager gameManager; // Reference to the GameManager

  

  public void IncreaseScore(int amountToAdd)
   {
    if (!gameManager.IsGameOver)
    {
      score += amountToAdd;
      UpdateScoreText();
    }
   }

   private void UpdateScoreText()
   {
      scoreText.text = $"Score: {score}";
  }

}
