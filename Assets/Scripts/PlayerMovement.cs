using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

   //  Parameters
   [SerializeField, Range(1000.0f, 10000.0f)] private float speed = 1000.0f;
   [SerializeField, Range(1.0f, 10.0f)] private float tutorialSpeed = 1.0f;
   [SerializeField, Range(10.0f, 50.0f)] private float jumpForce = 10.0f;
   

   // Cache
   Rigidbody rb;
   Vector2 movement;
   Vector2 movementForce;
   // State


  private void Start()
  {
     rb = GetComponent<Rigidbody>();
     movement = Vector2.zero;
  }

  private void FixedUpdate()
  {
    HandleMovementTutorial();
  }

  private void HandleMovementTutorial()
  {

    // Declare and initialize variables
    Vector3 currPosition = rb.position;
    Vector3 moveDir = new Vector3(movement.x, 0, movement.y);
    Vector3 newPosition = ( currPosition + (moveDir * (tutorialSpeed * Time.fixedDeltaTime)) );

    rb.MovePosition(newPosition);
  }

  private void HandleMovement()
  {
    movementForce = new Vector3(movement.x * speed * Time.fixedDeltaTime, movement.y * speed * Time.fixedDeltaTime, 0);
    Debug.Log($"Movement force: {movementForce}");
    // Apply movement force
    rb.AddForce(movementForce);
  }

  public void Move(InputAction.CallbackContext context)
   {
      movement = context.ReadValue<Vector2>();
      Debug.Log($"Movement input: {movement}");
   }



}
