using UnityEngine;

public class Checkpoint : MonoBehaviour
{

   [Header("Game Parameters")]
   [SerializeField] private float checkpointTime = 10.0f; // Time to add when passing a checkpoint

  GameManager gameManager; // Reference to the ScoreManager for score updates

  private void Start()
  {
     gameManager = FindFirstObjectByType<GameManager>();    
  }


  private void OnTriggerEnter(Collider other)
  {
        if (other.CompareTag("Player"))
        {
            // Assuming you have a GameManager or similar to handle checkpoints
            gameManager.AddTime(checkpointTime);
        }
  }
}
