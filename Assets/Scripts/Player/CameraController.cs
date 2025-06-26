using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Game Parameters")]
    [SerializeField] private ParticleSystem speedUpParticleSystem;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private float minFOV = 20f;
    [SerializeField] private float maxFOV = 120f;
    [SerializeField] private float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifier = 5.0f; // Speed adjustment when hit

  private void Awake()
  {
     cinemachineCamera = GetComponent<CinemachineCamera>();
  }

  public void ChangeCameraFOV(float speedAmount)
  {   
      StopAllCoroutines();
      StartCoroutine(ChangeFOVRoutine(speedAmount));

      if(speedAmount > 0 && speedUpParticleSystem != null)
      {
              speedUpParticleSystem.Play();
      }

  }

  IEnumerator ChangeFOVRoutine(float changeInSpeed)
  {
 
     float startFOV = cinemachineCamera.Lens.FieldOfView;
     
     float targetFOV = Mathf.Clamp(startFOV + (changeInSpeed * zoomSpeedModifier), minFOV, maxFOV);


     float elapsedTime = 0f;
     while(elapsedTime < zoomDuration)
     { 
          elapsedTime += Time.deltaTime;
          float t = elapsedTime / zoomDuration;
          cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
          yield return null;
        
      }

      cinemachineCamera.Lens.FieldOfView = targetFOV; // Ensure we set the final value
  }

}
