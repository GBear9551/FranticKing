using UnityEngine;

public class Coin : PickUp
{

  [Header("Coin Settings")]
  [SerializeField] private int coinValue = 100; // Value of the coin, can be used to increase player's score or currency
  

  // Cache - Reference I.E. Dependencies
  ScoreManager scoreManager;

  public void Init(ScoreManager scoreMang)
  {
     scoreManager = scoreMang; // Cache the ScoreManager reference 
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {

      // Assuming the player has a method to pick up items
      PickUpItem(); 
    }
  }

  protected override void PickUpItem()
  {

    // Increase player's score or currency
      scoreManager.IncreaseScore(coinValue);

    // Implement specific behavior for picking up a coin
    Debug.Log("Coin picked up by player!");
    Destroy(this.gameObject);
    // You can add code here to increase player's score or currency
    //base.PickUpItem(); // Call the base method if needed
  }
}
