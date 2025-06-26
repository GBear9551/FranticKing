using System.Collections;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
  [Header("Parameters")]
  [SerializeField] float collisionCooldown = 1f; // Cooldown time in seconds
  [SerializeField] float collisionChunkSpeedAdjustment = -1.0f; // Speed adjustment when hit

  [Header("Cache")]
  [SerializeField] public LevelGenerator levelGenerator; // Reference to the LevelGenerator
  [SerializeField] Animator animator;
  [SerializeField] public ScoreManager scoreManager; // Reference to the ScoreManager
  const string hitString = "Hit";

  // State to prevent multiple hits in quick succession
  [Header("State")]
  public bool isHit = false;

  private void OnCollisionEnter(Collision other)
  {

    if (!isHit)
    {
      animator.SetTrigger(hitString);
      isHit = true;
      StartCoroutine(ResetHitStateRoutine());
      if(levelGenerator != null)
      {
        // Adjust the speed of the level generator when hit
        levelGenerator.AdjustChunkSpeed(collisionChunkSpeedAdjustment); // Speed adjustment when hit
      }
      else
      {
        Debug.LogWarning("LevelGenerator reference is not set in PlayerCollisionHandler.");
      }
    }
    
  }

  private IEnumerator ResetHitStateRoutine()
  {
    yield return new WaitForSeconds(collisionCooldown); // Adjust the delay as needed
    isHit = false;
  }


}
