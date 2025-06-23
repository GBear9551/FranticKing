using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
  
   [SerializeField] GameObject chunkPrefab;
   [SerializeField] public int numOfChunks = 12;
   [SerializeField] Transform chunkParent;
   [SerializeField, Range(0,1)] public float gap = 0.0f;
   public List<GameObject> chunks = new List<GameObject>();
   int chunksCreated = 0;



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
    GameObject chunk = Instantiate(chunkPrefab, transform.position + offset, Quaternion.identity, chunkParent);
    chunk.GetComponent<ChunkMovement>().levelGenerator = this;
    chunksCreated++;
    chunk.name = "Chunk " + (chunksCreated).ToString();
    chunks.Add(chunk);
    return chunk;
  }

}
