using UnityEngine;

public class Apple : PickUp
{

  // Parameters
  [SerializeField] private float adjustGameSpeed = 2.0f;

  // Cache
  public LevelGenerator levelGenerator;


  public void Init(LevelGenerator levelGenerator)
  {
      this.levelGenerator = levelGenerator; // Cache the LevelGenerator reference
      if (levelGenerator == null)
      {
          Debug.LogError("LevelGenerator reference is not set in Apple script.");
      }
  }

  private void OnTriggerEnter(Collider other)
  {
      if (other.CompareTag("Player"))
      {
          // Call the base method to handle the pickup
          levelGenerator.AdjustChunkSpeed(adjustGameSpeed); 
      }
  }

  protected override void PickUpItem()
  {
      // Implement specific behavior for picking up an apple
      Debug.Log("Apple picked up by player!");
      // You can add code here to increase player's health or score
      //base.PickUpItem(); // Call the base method if needed
  }
}

