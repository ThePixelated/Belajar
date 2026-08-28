using System.Collections.Generic;
using UnityEngine;

public class ChunkMovement : MonoBehaviour
{
    [SerializeField] private ChunkManager m_chunkGenerator;
    [SerializeField] private float speedMovement;

    private Vector3 _direction = Vector3.back;

    private void Update()
    {
        if (GameManager.CurrentGameState == GameState.PLAYING)
        {
            transform.position += _direction * speedMovement * Time.deltaTime;
        }
    }
}
