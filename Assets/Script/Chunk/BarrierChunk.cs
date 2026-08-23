using UnityEngine;

public class BarrierChunk : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FrontConnection"))
        {
            Debug.Log("Collision Detected");
            ChunkManager.BarrierChunkDetection();
        }
    }
}
