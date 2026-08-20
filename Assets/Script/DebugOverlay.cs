using UnityEngine;

public class DebugOverlay : MonoBehaviour
{
    //[SerializeField] private Font monoFont;
    private PlayerManager _playerManager;
    private void Start()
    {
        _playerManager = PlayerManager.instance;
    }

    private string stringToEdit = "";
    private GUIStyle guiStyle;
    void OnGUI()
    {
        if (guiStyle == null)
        {
            guiStyle = new GUIStyle(GUI.skin.label);
            //guiStyle.font = monoFont;
            guiStyle.fontSize = 40;
            guiStyle.wordWrap = true;
        }

        guiStyle.normal.textColor = Color.yellow;
        GUI.Label(new Rect(50, 250, Screen.width, 50), $"<b>Sensitivity Status: {_playerManager.CurrentSens}</b>", guiStyle);

        guiStyle.normal.textColor = Color.white;
        stringToEdit = $"Camera World Position      : ({_playerManager.WorldCam.position.x},{_playerManager.WorldCam.position.y})\n" +
                       $"Player World Position        : ({_playerManager.Player.position.x},{_playerManager.Player.position.y})\n" +
                       $"Tap-Point Screen Position  : ({_playerManager.LastMousePos.x},{_playerManager.LastMousePos.y})";
        GUI.Label(new Rect(50, 300, Screen.width, 400), stringToEdit, guiStyle);
    }
}
