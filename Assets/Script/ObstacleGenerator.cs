using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private StateChunk currentChunk = StateChunk.NORMAL;
    [SerializeField] private StateChunk targetChunk = StateChunk.NORMAL;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float spawnCountdown;
    [SerializeField] private bool isEventTrigger;
    [SerializeField] private int lengthListChunk;
    [SerializeField] private List<ListOfChunks> listOfChunk = new List<ListOfChunks>();
    private int _idChunk = 0;
    private void Awake()
    {
        spawnCountdown = 0;

        if (spawnCountdown <= 0)
        {
            spawnCountdown = spawnCooldown;
            for (int i = 0; i < lengthListChunk; i++) // abritary number
            {
                listOfChunk.Add(new ListOfChunks());
                listOfChunk[i].ChunkState = targetChunk;
                listOfChunk[i].ChunkID = _idChunk.ToString();
                _idChunk++;
            }
            currentChunk = listOfChunk[0].ChunkState;
        }
    }

    private void Update()
    {
        spawnCountdown -= Time.deltaTime;
        if (spawnCountdown < 0)
        {
            spawnCountdown = spawnCooldown;
            listOfChunk.RemoveAt(0);

            listOfChunk.Add(new ListOfChunks());

            ChunkSet();

            int lastIndex = listOfChunk.Count;
            listOfChunk[lastIndex - 1].ChunkState = targetChunk;
            listOfChunk[lastIndex - 1].ChunkID = _idChunk.ToString();
            _idChunk++;
            currentChunk = listOfChunk[0].ChunkState;
        }
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

    public void TriggerEvent()
    {
        if(!isEventTrigger)
        {
            isEventTrigger = true;
            //ChunkSet();
        }
    }

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
            
            if (i < listOfChunk.Count - 1) listInfo += ",";
        }
        listInfo += "]";

        infoPanel = $"Current Chunk: {currentChunk}\n" +
                    $"Target Chunk: {targetChunk}\n" +
                    $"Event Trigger: {isEventTrigger}\n" +
                    $"New chunk countdown: {spawnCountdown:.}\n" +
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
    public StateChunk ChunkState;
    public string ChunkID;
}