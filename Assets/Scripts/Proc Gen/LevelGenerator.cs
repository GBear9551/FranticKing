using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
   [Header("Parameters")] 
   [SerializeField] List<GameObject> chunkPrefabs;
   [SerializeField] GameObject checkpointChunkPrefab; 
   
   [Tooltip("Number of chunks in the level at a given time.")]
   [SerializeField] public int numOfChunks = 12;
   [SerializeField] Transform chunkParent;
   [SerializeField, Range(0,1)] public float gap = 0.0f;
   [SerializeField, Range(3, 20)] public int specialChunkSpawnInterval = 10; // Interval for special chunk spawning


  [Header("Cache")]
   [SerializeField] public CameraController cameraController; // Reference to the CameraController for FOV changes
   [SerializeField] public ScoreManager scoreManger; // Reference to the ScoreManager for score updates


  [Header("Level Settings")]
   [SerializeField, Range(1.0f,10.0f)] public float maxChunkSpeed = 5.0f;
   [SerializeField, Range(0.1f, 5.0f)] public float minChunkSpeed = 1.0f; // Minimum speed for chunks

   public List<GameObject> chunks = new List<GameObject>();
   [SerializeField] public float currentSpeed = 2.0f;
   int chunksCreated = 0;
  

  public void AdjustChunkSpeed(float speedAdjustment)
  {


    // Adjust the speed of each chunk
    currentSpeed += speedAdjustment; // Adjust the current speed of the level generator

    // Clamp the speed to the defined limits
    currentSpeed = Mathf.Clamp(currentSpeed, minChunkSpeed, maxChunkSpeed);

    // Camera Visual Effects
    if(cameraController != null)
    {
      cameraController.ChangeCameraFOV(speedAdjustment); // Adjust camera FOV based on speed      
    }
    else
    {
      Debug.LogWarning("CameraController reference is not set in LevelGenerator.");
      return; // Exit if cameraController is not set
    }


    // Loop through all chunks and adjust their speed
    foreach (GameObject chunk in chunks)
    {
      var chunkMoveSpeed = chunk.GetComponent<ChunkMovement>();

      if (chunkMoveSpeed != null )
      {

        chunkMoveSpeed.AdjustChunkSpeed(currentSpeed); // Example adjustment, can be parameterized

        // Change Physics settings if needed
        // Physics.gravity = new Vector3(0, 0,-9.81f - currentSpeed); // Example of changing gravity based on speed


      }
      else
      {
        Debug.LogWarning("ChunkMovement component not found on chunk: " + chunk.name);
      }

    }
  }

  private void Start()
  {

    // Declare and initialize variables.
    int index = 0;
    Vector3 offset = new Vector3(0,0,10+gap);

    // Loop to generate the specified number of chunks
    for (index = 0; index < numOfChunks; index++)
    {
      // Generate chunks in a straight line along the z-axis
      GenerateChunk(offset*index);
    }

  }

  public GameObject GenerateChunk(Vector3 offset)
  {
    // Check if chunkPrefabs is empty
    if (chunkPrefabs.Count == 0) { Debug.LogError("No Chunk Prefabs to generate!"); return this.gameObject; }



    var chunkPrefab = ChunkPrefabSelector(); // Randomly select a chunk prefab from the list

    GameObject chunk = Instantiate(chunkPrefab, transform.position + offset, Quaternion.identity, chunkParent);


    chunk.GetComponent<Chunk>().Init(this,scoreManger); // Initialize the chunk with a reference to the LevelGenerator

    // Set the chunk's speed based on the current speed of the level generator
    var chunkMovement = chunk.GetComponent<ChunkMovement>();
    chunkMovement.levelGenerator = this;
    chunkMovement.AdjustChunkSpeed(currentSpeed); // Set the initial speed of the chunk
    
    // Increase number of chunks created
    chunksCreated++;
    

    chunk.name = "Chunk " + (chunksCreated).ToString();
    
    chunks.Add(chunk);
    return chunk;
  }

  private GameObject ChunkPrefabSelector()
  {

    // Every set number of chunks, spawn a special chunk, called the chunky chunk checkpoint.
    if(chunksCreated % specialChunkSpawnInterval == 0 && chunksCreated != 0)
    {
      // Check if the special chunk prefab exists in the list
      if (checkpointChunkPrefab != null)
      {
        return checkpointChunkPrefab; // Return the special chunk prefab
      }
      else
      {
        Debug.LogWarning("Special Chunky Checkpoint Chunk Prefab not found in the list. Returning a regular chunk prefab.");
      }
    }
    // Randomly select a chunk prefab from the list
    int randomIndex = Random.Range(0, chunkPrefabs.Count);
    return chunkPrefabs[randomIndex];


  }


}
