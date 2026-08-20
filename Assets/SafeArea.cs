using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform rectTrans;
    private Rect lastSafeArea;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();

        Apply();
    }

    private void Update()
    {
        if (Screen.safeArea != lastSafeArea)
        {
            Apply();
        }
    }

    private void Apply()
    {
        // target size width = 978.43f; height = 1896.958f;
        //var safeAreaForUIToolkit = new Rect(Screen.safeArea.x, Screen.height - Screen.safeArea.y, Screen.safeArea.width, Screen.safeArea.height);
        lastSafeArea = Screen.safeArea;
        Screen.brightness = 0.2f;
        Debug.Log($"{lastSafeArea.height} - {(Screen.height - lastSafeArea.height)} : {lastSafeArea.height - (Screen.height - lastSafeArea.height)}");
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        //Debug.Log(safeAreaForUIToolkit);
        Debug.Log(lastSafeArea);
        rectTrans.sizeDelta = new Vector2(lastSafeArea.width, lastSafeArea.height - (Screen.height - lastSafeArea.height));
        rectTrans.position = Vector3.zero;
    }
}
