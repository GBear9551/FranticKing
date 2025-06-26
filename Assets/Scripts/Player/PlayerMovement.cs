using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

   //  Parameters
   [SerializeField, Range(1000.0f, 10000.0f)] private float speed = 1000.0f;
   [SerializeField, Range(1.0f, 10.0f)] private float tutorialSpeed = 1.0f;
   //[SerializeField, Range(10.0f, 50.0f)] private float jumpForce = 10.0f;
   

   // Cache
   Rigidbody rb;
   Vector2 movement;
   Vector3 movementForce;
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

  private void HandleMovement()
  {
    movementForce = new Vector3(movement.x * speed * Time.fixedDeltaTime,0, movement.y * speed * Time.fixedDeltaTime);
    //rb.AddForce(movementForce);
    Debug.Log($"Movement force: {movementForce}");
    // Apply movement force
    Debug.Log($"Player position: {rb.position}");
    if(rb.position.z < -1.0f || rb.position.z > 3.0f)
    {
      rb.linearVelocity = Vector3.zero; // Reset angular velocity to prevent spinning
      float zpos = Mathf.Clamp(rb.position.z, -1.0f, 3.0f);
      // Reset position if below a certain threshold
      rb.position = new Vector3(rb.position.x, rb.position.y, zpos);
      rb.AddForce(new Vector3(movementForce.x, 0, 0));
    }
    else
    {
       rb.AddForce(movementForce);
    }


  }
  public void Move(InputAction.CallbackContext context)
   {
      movement = context.ReadValue<Vector2>();
      //Debug.Log($"Movement input: {movement}");
   }

  private void HandleMovementTutorial()
  {

    // Declare and initialize variables
    Vector3 currPosition = rb.position;
    Vector3 moveDir = new Vector3(movement.x, 0, movement.y);
    Vector3 newPosition = ( currPosition + (moveDir * (tutorialSpeed * Time.fixedDeltaTime)) );
    newPosition.x = Mathf.Clamp(newPosition.x, -4.0f, 4.5f); // Clamp X position between -5 and 5
    newPosition.z = Mathf.Clamp(newPosition.z, -1.0f, 3.0f); // Clamp Z position between -1 and 3
    rb.MovePosition(newPosition);
  }




}
