using UnityEngine;

public class PickUp : MonoBehaviour
{

  [SerializeField, Range(-360.0f, 360.0f)] private float rotationRate = 50f; // Degrees per second for rotation effect

  private void Update()
  {
    // Rotate the pickup item for visual effect on the y axis
    transform.Rotate(0, rotationRate * Time.deltaTime, 0, Space.World);
  }

  private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         // Assuming the player has a method to pick up items
         PickUpItem(); 
      }
  }

  protected virtual void PickUpItem()
  {
    // This method can be overridden by derived classes to implement specific pickup behavior
    Debug.Log("Item picked up by player: " + gameObject.name);
  }

}
