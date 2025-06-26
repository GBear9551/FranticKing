using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
  [SerializeField] private GameObject applePrefab;
  [SerializeField] private GameObject coinPrefab;
  [SerializeField] private GameObject fencePrefab;
  [SerializeField] private float[] lanes = { -2.5f, 0.0f, 2.5f };
  [SerializeField,Range(0.5f,2.5f)] private float zCoinOffset = 1.0f; // Offset for coin spawning 
  List<int> availableLanes = new List<int>{ 0, 1, 2 };
  LevelGenerator levelGenerator;
  ScoreManager scoreManager;

  public void Init(LevelGenerator levelGenerator, ScoreManager scoreMang)
  {
    this.scoreManager = scoreMang; // Cache the ScoreManager reference
    this.levelGenerator = levelGenerator; // Cache the LevelGenerator reference
    if (levelGenerator == null)
    {
      Debug.LogError("LevelGenerator reference is not set in Chunk script.");
    }

    if (scoreManager == null)
    {
      Debug.LogError("ScoreManager reference is not set in Chunk script.");
    }

  }

  private void Start()
  {
    // Create a fence around the chunk
    SpawnFence();

    // Spawn apple pick up
    SpawnApple();

    // Spawn coin pick ups
    SpawnCoins();
    
  }

  private void SpawnCoins()
  {
    // Declare and initialize variables
    int numOfCoins = Random.Range(1, 4); // Randomly choose between 1 to 3 coins
    int index = 0; // Index for coin spawning
    // Randomly select a lane index
    int randLaneIndex = Random.Range(0, availableLanes.Count);
    
    if(availableLanes.Count == 0)
    {
      Debug.LogWarning("No available lanes to spawn coins.");
      return;
    }


    int selectedLane = availableLanes[randLaneIndex];
    float randLanePos = lanes[selectedLane];
    availableLanes.Remove(selectedLane);

    for (index = 0; index < numOfCoins; index++)
    {
      Vector3 spawnPosition = new Vector3(randLanePos, transform.position.y, transform.position.z + (zCoinOffset*index));
      
      // Instantiate the coin prefab at the random lane position
      GameObject coinGO = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, transform);
      var coinComp = coinGO.GetComponent<Coin>();

      if (coinComp != null)
      {
        // Assign the level generator to the coin component
        coinComp.Init(scoreManager);
      }
      else
      {
        Debug.LogError("Coin component not found on the coin prefab.");
      }

    }
  }

  private void SpawnApple()
  {
    // Declare and initialize variables
    int randLaneIndex = Random.Range(0, availableLanes.Count);
    int selectedLane = availableLanes[randLaneIndex];
    float randLanePos = lanes[selectedLane];
    Vector3 spawnPosition = new Vector3(randLanePos, transform.position.y, transform.position.z);
    
    // Instantiate the apple prefab at the random lane position
    var apple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, transform);

    // Assign the apple to the chunk's level generator.
    apple.GetComponent<Apple>().levelGenerator = GetComponentInParent<LevelGenerator>();

    availableLanes.Remove(selectedLane);
  }

  private void SpawnFence()
  {

    // Declare and initialize variables
    int index = 0;
    int numOfFences = Random.Range(0, lanes.Length);


    for (index = 0; index < numOfFences; index++)
    {

      int randLaneIndex = Random.Range(0, availableLanes.Count);
      int selectedLane = availableLanes[randLaneIndex]; 

      float randLanePos = lanes[selectedLane];
      Vector3 spawnPosition = new Vector3(randLanePos, transform.position.y, transform.position.z);
      Instantiate(fencePrefab, spawnPosition, Quaternion.identity, transform);
      availableLanes.Remove(selectedLane);
    }
  }

}
