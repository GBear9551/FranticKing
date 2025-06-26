using UnityEngine;
using System.Collections; // Required for using IEnumerator
public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] obstaclePrefabs; // Prefab for the obstacle to spawn
    [SerializeField] private float spawnInterval = 2f; // Time interval between spawns
    [SerializeField] private Transform obstaclesParent;
    [SerializeField, Range(5.0f,30.0f)] private float difficultyUpdateInterval = 5.0f; // Time interval to update difficulty level


  //[SerializeField] //
  // Start is called once before the first execution of Update after the MonoBehaviour is created

  // Difficulty level can be used to adjust the spawn rate or other parameters based on game difficulty
  int difficultyLevel = 1; // Default difficulty level

   IEnumerator UpdateDifficultyRoutine()
   {
      yield return new WaitForSeconds(difficultyUpdateInterval); // Wait for 5 seconds before updating difficulty
      difficultyLevel++; // Increase the difficulty level
      spawnInterval -= 0.1f; // Decrease the spawn interval to make it harder
      spawnInterval = Mathf.Max(spawnInterval, 0.5f); // Ensure the spawn interval does not go below 0.5 seconds
      // Track the time spent at the hardest difficulty.
      // SpawnInterval == 0.5f , randomize the spawn rate of obstacles

      StartCoroutine(UpdateDifficultyRoutine()); // Restart the coroutine to keep updating difficulty
  }

    void Start()
    {
        
       StartCoroutine(SpawnObstacleRoutine()); // Start the coroutine to spawn obstacles
       StartCoroutine(UpdateDifficultyRoutine()); // Start the coroutine to update difficulty level                                               
  }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnObstacleRoutine()
    {

        while (true) // Infinite loop to keep spawning obstacles
        {
            var obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]; // Randomly select an obstacle prefab from the array
            var randomPosition = new Vector3(Random.Range(-4f, 4f), transform.position.y, transform.position.z); // Generate a random position within a specified range
            Instantiate(obstaclePrefab, randomPosition, Random.rotation, obstaclesParent ); // Spawn the obstacle at the spawner's position
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval before spawning the next obstacle
        }

  }
 
}
