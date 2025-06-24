using UnityEngine;
using System.Collections; // Required for using IEnumerator
public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] obstaclePrefabs; // Prefab for the obstacle to spawn
    [SerializeField] private float spawnInterval = 2f; // Time interval between spawns

                                                        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
       StartCoroutine(SpawnObstacleRoutine()); // Start the coroutine to spawn obstacles      
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
            Instantiate(obstaclePrefab, randomPosition, Random.rotation); // Spawn the obstacle at the spawner's position
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval before spawning the next obstacle
        }

  }
 
}
