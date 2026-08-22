using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    //public static ChunkManager Instance;

    [SerializeField] private GameObject prefabChunk;
    [SerializeField] private Transform spawnPoint;
    //[SerializeField] private float chunkMovSpeed;
    [SerializeField] private StateChunk currentChunk = StateChunk.NORMAL;
    [SerializeField] private StateChunk targetChunk = StateChunk.NORMAL;
    //[SerializeField] private float spawnCooldown;/
    //[SerializeField] private float spawnCountdown;
    [SerializeField] private bool isEventTrigger;
    [SerializeField] private int lengthListChunk;
    [SerializeField] private List<ListOfChunks> listOfChunk = new List<ListOfChunks>();
    private int _idChunk = 0;
    private int _currentIndexChunkPool = 0;
    public List<ListOfChunks> ListOfChunks { get { return listOfChunk; } set { listOfChunk = value; } }

    private void Awake()
    {
        //Instance = this;
        Innitiate();
    }

    //private void Update()
    //{
        

    //    if (listOfChunk.Count < lengthListChunk)
    //    {
    //        SpawnChunk();
    //    }
    //}

    private void OnEnable()
    {
        onBarrierChunk += PoolingChunk;
    }

    private void OnDisable()
    {
        onBarrierChunk -= PoolingChunk;
    }

    public static Action onBarrierChunk;
    public static void BarrierChunkDetection()
    {
        onBarrierChunk?.Invoke();
    }

    //private void RemoveChunk(GameObject targetGObj)
    //{
    //    Destroy(listOfChunk[0].ChunkObj);
    //    listOfChunk[0].ChunkObj = null;
    //    listOfChunk.RemoveAt(0);
    //}

    private void Innitiate()
    {
        for (int i = 0; i < lengthListChunk; i++) SpawnChunk();
    }

    private void SpawnChunk()
    {
        listOfChunk.Add(new ListOfChunks());
        //ChunkSet();

        listOfChunk[listOfChunk.Count - 1].ChunkState = targetChunk;
        listOfChunk[listOfChunk.Count - 1].ChunkID = _idChunk.ToString();
        _idChunk++;

        GameObject gObj = Instantiate(prefabChunk);
        listOfChunk[listOfChunk.Count - 1].ChunkObj = gObj;

        gObj.name = "Chunk_" + listOfChunk[listOfChunk.Count - 1].ChunkID + ": " + listOfChunk[listOfChunk.Count - 1].ChunkState;
        gObj.transform.position = spawnPoint.position;


        if (listOfChunk.Count > 1)
        {
            Vector3 childChunk = gObj.GetComponent<ChunkInformation>().LeftPartPref.transform.localScale;
            Debug.Log($"LS {gObj.name}: " + childChunk);
            Debug.Log($"Trans Pos: {listOfChunk[(listOfChunk.Count - 1) - 1].ChunkObj.name}" + listOfChunk[(listOfChunk.Count - 1) - 1].ChunkObj.transform.position);

            Vector3 targetPos = new Vector3(gObj.transform.position.x, gObj.transform.position.y, listOfChunk[(listOfChunk.Count - 1) - 1].ChunkObj.transform.position.z + childChunk.z);
            //Vector3 targetPos = listOfChunk[(listOfChunk.Count - 1) - 1].ChunkObj.transform.position;
            gObj.transform.position = targetPos;
        }

        currentChunk = listOfChunk[0].ChunkState;
    }

    private void PoolingChunk()
    {
        ChunkSet();

        //0 2
        //1 0
        //2 1

        if (_currentIndexChunkPool >= lengthListChunk)
            _currentIndexChunkPool = 0;

        listOfChunk[_currentIndexChunkPool].ChunkState = targetChunk;
        listOfChunk[_currentIndexChunkPool].ChunkID = _currentIndexChunkPool.ToString();

        //int chunkPooledIndex = _currentIndexChunkPool;
        int chunkTargetIndex = (_currentIndexChunkPool + (lengthListChunk - 1)) % lengthListChunk;
        GameObject gObj = listOfChunk[_currentIndexChunkPool].ChunkObj;
        Debug.LogWarning($"CTI: {_currentIndexChunkPool},{chunkTargetIndex}");
        Vector3 childChunk = gObj.GetComponent<ChunkInformation>().LeftPartPref.transform.localScale;
        Debug.Log($"LS {gObj.name}: " + childChunk);
        Debug.Log($"Trans Pos: {listOfChunk[chunkTargetIndex].ChunkObj.name}" + listOfChunk[chunkTargetIndex].ChunkObj.transform.position);

        Vector3 targetPos = new Vector3(gObj.transform.position.x, gObj.transform.position.y, listOfChunk[chunkTargetIndex].ChunkObj.transform.position.z + childChunk.z);
        //Vector3 targetPos = listOfChunk[(listOfChunk.Count - 1) - 1].ChunkObj.transform.position;
        gObj.transform.position = targetPos;

        currentChunk = listOfChunk[_currentIndexChunkPool].ChunkState;
        _currentIndexChunkPool++;
    }

    private void ChunkSet()
    {
        if (isEventTrigger && targetChunk == StateChunk.NORMAL) { targetChunk = StateChunk.READY; }
        else if (isEventTrigger && targetChunk == StateChunk.READY) { targetChunk = StateChunk.ACTIVE; }
        else if (isEventTrigger && targetChunk == StateChunk.ACTIVE) { targetChunk = StateChunk.GRACE; }
        else if (isEventTrigger && targetChunk == StateChunk.GRACE)
        {
            targetChunk = StateChunk.NORMAL;
            isEventTrigger = false;
        }
    }

    public void TriggerEvent() { if(!isEventTrigger) isEventTrigger = true; }


    string infoPanel;
    GUIStyle gStyle;
    private void OnGUI()
    {
        if (gStyle == null)
        {
            gStyle = new GUIStyle();
            gStyle.fontSize = 35;
            gStyle.normal.textColor = Color.yellow;
            gStyle.fontStyle = FontStyle.Bold;
            gStyle.wordWrap = true;
        }

        string listInfo = "[";
        for (int i = 0; i < listOfChunk.Count; i++)
        {
            listInfo += listOfChunk[i].ChunkID + "_" + listOfChunk[i].ChunkState;
            
            if (i < listOfChunk.Count - 1) listInfo += ", ";
        }
        listInfo += "]";

        infoPanel = $"Current Chunk: {currentChunk}\n" +
                    $"Target Chunk: {targetChunk}\n" +
                    $"Event Trigger: {isEventTrigger}\n" +
                    $"List of chunk [{listOfChunk.Count}] : {listInfo}";

        GUI.TextArea(new Rect(30, 600, Screen.width, 300), infoPanel, gStyle);
    }
}

public enum StateChunk
{
    NORMAL,
    READY,
    ACTIVE,
    GRACE,
}

[System.Serializable]
public class ListOfChunks
{
    public GameObject ChunkObj;
    public StateChunk ChunkState;
    public string ChunkID;
}