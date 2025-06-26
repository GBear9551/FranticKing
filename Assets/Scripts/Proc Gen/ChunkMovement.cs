using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChunkMovement : MonoBehaviour
{
    
    // Parameters
    [Tooltip("Speed of chunks is modifier via level generator, to address the whole collection.")] private float speed = 1.0f;

    // Cache
    public LevelGenerator levelGenerator = null;

    // State 

    public void AdjustChunkSpeed(float adjustment)
    {
        speed = adjustment;
    }

    // Update is called once per frame
    void Update()
    {

       // Declare and initialize variables

       // Update the position of the chunk
       transform.position += -Vector3.forward * (speed * Time.deltaTime);

        // If the chunk is far enough away, destroy it
        if (transform.position.z < -10f)
        {

            Vector3 posOfEndChunk = levelGenerator.chunks[levelGenerator.chunks.Count - 1].transform.position;
            // Create a new chunk at the end of the list
            Vector3 offset = new Vector3(0, 0, posOfEndChunk.z + 10);
            levelGenerator.GenerateChunk(offset);
            DestroyChunk();
          
        }
  }

  private void DestroyChunk()
  {
    // Remove the chunk from the level generator's list and destroy it
    levelGenerator.chunks.Remove(this.gameObject);
    levelGenerator.numOfChunks--;
    Destroy(gameObject);
  }
}
