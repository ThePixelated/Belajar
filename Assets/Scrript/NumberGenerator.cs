//using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumberGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform gridBorder;
    [SerializeField] private GameObject prefabBackgroundCell;
    [SerializeField] private GameObject prefabCell;
    [SerializeField] private float maxSize = 200;
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] private float timer;
    [SerializeField] private TargetCellGrid targetCellGrid;
    [SerializeField] private List<GameObject> allCell = new List<GameObject>();
    //[SerializeField] private List<int> gridNumber = new List<int>();


    private int prevAmount;

    private void Awake()
    {
        //Debug.Log(gridBorder.sizeDelta.x);
        //Debug.Log(gridBorder.sizeDelta.y);
        //Debug.Log("");
        //Debug.Log(gridBorder.rect.width);
        //Debug.Log(gridBorder.rect.height);
        //Debug.Log("");


        //GridGenerator();
        //GenerateCell();
        //prevAmount = targetCellGrid.Amount;
    }

    void Start()
    {
        // cek prime apa bukan
        // kalo iya langsung bagi 2 aja
        // kalo ga 
    }

    void Update()
    {
        if (timer <= 0.5)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            if (prevAmount != targetCellGrid.Amount)
            {
                RemoveCell();
                GridGenerator();
                GenerateCell();
                prevAmount = targetCellGrid.Amount;
            }

            timer -= Time.deltaTime;
            timerTxt.text = ((int)timer).ToString();
        }
    }

    public void GridGenerator()
    {
        int strike = 0;
        List<Vector2Int> numVal = new List<Vector2Int>();

        for (float i = 1; i <= targetCellGrid.Amount; i++)
        {
            if ((targetCellGrid.Amount / i) == (int)(targetCellGrid.Amount / i))
            {
                Vector2Int newVal = new Vector2Int((int)i, (int)(targetCellGrid.Amount / i));
                Debug.Log($"pembagian bulat ({targetCellGrid.Amount} / {i}): " + newVal.x + ", " + newVal.y);
                numVal.Add(newVal);
                strike++;
            }
        }

        // not prime
        int smallestIndex = 0;
        if (strike > 2)
        {
            int minVal = Mathf.Abs(numVal[0].x - numVal[0].y);
            for (int i = 1; i < numVal.Count; i++)
            {
                int subtraction = Mathf.Abs(numVal[i].x - numVal[i].y);
                if (subtraction <= minVal)
                {
                    minVal = subtraction;
                    smallestIndex = i;
                    Debug.Log($" sub: {subtraction}; minVal: {minVal} - [{smallestIndex}]");
                }
            }
        }

        targetCellGrid.targetGridDiv = numVal[smallestIndex];
        numVal.Clear();
        for (int i = 1; i <= targetCellGrid.Amount; i++)
            targetCellGrid.GridValue.Add(i);

        for (int i = 0; i < targetCellGrid.GridValue.Count; i++)
        {
            int randTargetIndex = Random.Range(0, targetCellGrid.GridValue.Count);
            int temp = targetCellGrid.GridValue[i];
            int tempRand = targetCellGrid.GridValue[randTargetIndex];
            //int
            targetCellGrid.GridValue[i] = tempRand;
            targetCellGrid.GridValue[randTargetIndex] = temp;
        }
    }

    public void GenerateCell()
    {
        float widthSize = gridBorder.rect.width / targetCellGrid.targetGridDiv.x;
        float heightSize = gridBorder.rect.height / targetCellGrid.targetGridDiv.y;

        Debug.LogWarning($"{widthSize},{heightSize}");
        Debug.Log("");
        if (widthSize >= maxSize)
            widthSize = maxSize;
        if (heightSize >= maxSize)
            heightSize = maxSize;

        int counter = 0;
        for (int height = 0; height < targetCellGrid.targetGridDiv.y; height++)
        {
            for (int width = 0; width < targetCellGrid.targetGridDiv.x; width++)
            {
                GameObject cell = Instantiate(prefabBackgroundCell, gridBorder);
                cell.name = "CellNum-" + targetCellGrid.GridValue[counter];
                //Debug.Log(cell.transform.position);

                RectTransform cellRect = cell.GetComponent<RectTransform>();

                Debug.LogWarning(cellRect.anchoredPosition);
                cellRect.sizeDelta = new Vector2((float)widthSize, (float)heightSize);
                cellRect.anchoredPosition = new Vector2((float)(width * widthSize), (float)(height * heightSize * -1));
                Debug.Log(cellRect.anchoredPosition);

                //cellRect.rect = new Rect();
                //Debug.Log(cellRect.rect.position);
                ////cellRect.rect.width = 5;
                ////cellRect.rect.x = width;
                //cellRect.rect.Set(width* widthSize, height*heightSize, widthSize, heightSize);
                //Debug.LogWarning($"({width* widthSize}, {height*heightSize}, {widthSize}, {heightSize})");
                //Debug.Log(cell.transform.position);
                //Debug.Log(cellRect.rect.position);

                Image cellImage = cell.GetComponent<Image>();
                cellImage.color = new Color((float)(Random.Range(0, 256f) / 255f), (float)(Random.Range(0, 256f) / 255f), (float)(Random.Range(0, 256f) / 255f), 1);

                counter++;

                allCell.Add(cell);
            }
        }
    }

    private void RemoveCell()
    {
        foreach (var item in allCell)
        {
            Destroy(item);
        }

        allCell.Clear();

        //targetCellGrid.Amount = 0;
        targetCellGrid.targetGridDiv = new Vector2Int();
        targetCellGrid.GridValue = new List<int>();
    }
}

[System.Serializable]
public class TargetCellGrid
{
    public int Amount;
    public Vector2Int targetGridDiv;
    public List<int> GridValue = new List<int>();
}