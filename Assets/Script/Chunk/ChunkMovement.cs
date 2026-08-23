using System.Collections.Generic;
using UnityEngine;

public class ChunkMovement : MonoBehaviour
{
    [SerializeField] private ChunkManager m_chunkGenerator;
    [SerializeField] private float speedMovement;

    //private List<ListOfChunks> _listofChunks;
    private Vector3 _direction = Vector3.back;

    private void Start()
    {
        //_listofChunks = m_chunkGenerator.ListOfChunks;
    }

    private void Update()
    {
        //foreach (var item in _listofChunks)
        //{
        //    if (item )
        //    {

        //    }
            transform.position += _direction * speedMovement * Time.deltaTime;
        //}
    }

    
}
