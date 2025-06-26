using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
  [SerializeField] private float collisionDelayStateWaitTime = 1.0f; // Adjust the duration as needed

  [SerializeField] private ParticleSystem collisionVFX; //
  [SerializeField] private AudioSource collisionSFX;
  CinemachineImpulseSource impulseSource;


  // State
  bool isColliding = false;

  private void Awake()
  {
    impulseSource = GetComponent<CinemachineImpulseSource>();
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (!isColliding)
    {
      FireImpulse();
      CollisionFX(collision);
      StartCoroutine(CollisionStateTimerRoutine());
    }
  }

  private void FireImpulse()
  {
    float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
    float impulseStrength = (1f / distance) * 2; // Adjust strength based on distance
    impulseStrength = Mathf.Clamp(impulseStrength, 0.1f, 1f); // Ensure strength is within a reasonable range
    impulseSource.GenerateImpulse(impulseStrength);
  }


  private void CollisionFX(Collision collision)
  {
    // Play collision effects here, e.g., particle systems, sound effects, etc.
    ContactPoint contactPoint = collision.contacts[0];

    collisionVFX.transform.position = contactPoint.point;
    collisionVFX.Play();
    collisionSFX.Play();


  }

  IEnumerator CollisionStateTimerRoutine()
  {
    isColliding = true;
    yield return new WaitForSeconds(collisionDelayStateWaitTime); // Adjust the duration as needed
    isColliding = false;
  }

}
