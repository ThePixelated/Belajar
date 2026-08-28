using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabChunk;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private StateChunk currentChunk = StateChunk.NORMAL;
    [SerializeField] private StateChunk targetChunk = StateChunk.NORMAL;
    [SerializeField] private bool isEventTrigger;
    [SerializeField] private int lengthListChunk;
    [SerializeField] private List<ListOfChunks> listOfChunk = new List<ListOfChunks>();
    private int _idChunk = 0;
    private int _currentIndexChunkPool = 0;
    public List<ListOfChunks> ListOfChunks { get { return listOfChunk; } set { listOfChunk = value; } }

    private void Awake()
    {
        Innitiate();
    }

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

    private void Innitiate()
    {
        for (int i = 0; i < listOfChunk.Count; i++) ReInnitiateChunk(i);
        currentChunk = listOfChunk[0].ChunkState;
    }

    private void ReInnitiateChunk(int i)
    {
        listOfChunk[i].ChunkState = targetChunk;
        listOfChunk[i].ChunkID = _idChunk.ToString();
        _idChunk++;

        GameObject gObj = listOfChunk[i].ChunkObj;

        //gObj.name = "Chunk_" + listOfChunk[i].ChunkID + ": " + listOfChunk[i].ChunkState;
        gObj.transform.position = spawnPoint.position;


        if (i > 0)
        {
            Vector3 childChunk = gObj.GetComponent<ChunkInformation>().LeftPartPref.transform.localScale;
            //Debug.Log($"LS {gObj.name}: " + childChunk);
            //Debug.Log($"Trans Pos: {listOfChunk[i - 1].ChunkObj.name}" + listOfChunk[i - 1].ChunkObj.transform.position);

            Vector3 targetPos = new Vector3(gObj.transform.position.x, gObj.transform.position.y, listOfChunk[i - 1].ChunkObj.transform.position.z + childChunk.z);
            gObj.transform.position = targetPos;
        }
    }

    private void PoolingChunk()
    {
        if (_currentIndexChunkPool >= listOfChunk.Count)
            _currentIndexChunkPool = 0;

        listOfChunk[_currentIndexChunkPool].ChunkState = targetChunk;
        listOfChunk[_currentIndexChunkPool].ChunkID = _currentIndexChunkPool.ToString();

        int chunkTargetIndex = (_currentIndexChunkPool + (listOfChunk.Count - 1)) % listOfChunk.Count;
        GameObject gObj = listOfChunk[_currentIndexChunkPool].ChunkObj;
        //Debug.LogWarning($"CTI: {_currentIndexChunkPool},{chunkTargetIndex}");
        Vector3 childChunk = gObj.GetComponent<ChunkInformation>().LeftPartPref.transform.localScale;
        //Debug.Log($"LS {gObj.name}: " + childChunk);
        //Debug.Log($"Trans Pos: {listOfChunk[chunkTargetIndex].ChunkObj.name}" + listOfChunk[chunkTargetIndex].ChunkObj.transform.position);

        Vector3 targetPos = new Vector3(gObj.transform.position.x, gObj.transform.position.y, listOfChunk[chunkTargetIndex].ChunkObj.transform.position.z + childChunk.z);
        gObj.transform.position = targetPos;

        currentChunk = listOfChunk[_currentIndexChunkPool].ChunkState;
        _currentIndexChunkPool++;

        ChunkSet();
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


    //string infoPanel;
    //GUIStyle gStyle;
    //private void OnGUI()
    //{
    //    if (gStyle == null)
    //    {
    //        gStyle = new GUIStyle();
    //        gStyle.fontSize = 35;
    //        gStyle.normal.textColor = Color.yellow;
    //        gStyle.fontStyle = FontStyle.Bold;
    //        gStyle.wordWrap = true;
    //    }

    //    string listInfo = "[";
    //    for (int i = 0; i < listOfChunk.Count; i++)
    //    {
    //        listInfo += listOfChunk[i].ChunkID + "_" + listOfChunk[i].ChunkState;
            
    //        if (i < listOfChunk.Count - 1) listInfo += ", ";
    //    }
    //    listInfo += "]";

    //    infoPanel = $"Current Chunk: {currentChunk}\n" +
    //                $"Target Chunk: {targetChunk}\n" +
    //                $"Event Trigger: {isEventTrigger}\n" +
    //                $"List of chunk [{listOfChunk.Count}] : {listInfo}";

    //    GUI.TextArea(new Rect(30, 600, Screen.width, 300), infoPanel, gStyle);
    //}
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

[System.Serializable]
public class ChunkByLevel
{
    public LevelMap LevelMap;
    public List<ChunkComponent> ChunkComponent;
}

[System.Serializable]
public class ChunkComponent
{
    public string Name;
    public ComponentType Type;
    public GameObject Component;
}

public enum ComponentType
{
    OBSTACLE,
    DECORATIVE,
    PlATE
}