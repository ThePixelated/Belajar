using System.Collections;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField] private PlayerPosition playerPosition = PlayerPosition.CENTER;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] pivotPlayerPos;
    [SerializeField] private float speedAnimation = 1.0f;
    private Transform _playerTrans;
    private Coroutine _playerMovementCor;
    private Vector3 targetPos;
    private float deltaTime;

    public PlayerPosition PlayerPosition { get; private set; } = PlayerPosition.CENTER;

    private void Awake()
    {
        //    Debug.LogWarning("INNITIATE");

            _playerTrans = player.transform;
            _playerTrans.position = pivotPlayerPos[(int)PlayerPosition].position;
    }

    private void Update()
    {
        if (GameManager.CurrentGameState == GameState.PLAYING)
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

            if (MobileSwipe.isReturnRightSwipe())
            {
                Debug.Log("Right Swipe");
                playerPosition++;
                PlayerPosition++;
                if (PlayerPosition > PlayerPosition.RIGHT)
                {
                    playerPosition = PlayerPosition.RIGHT;
                    PlayerPosition = PlayerPosition.RIGHT;
                }
                else
                {
                    if (_playerMovementCor == null)
                    {
                        Debug.LogWarning("Initiate Right Movement");
                        _playerMovementCor = StartCoroutine(PlayerMovement());
                    }
                    else
                    {
                        //_playerTrans.position = targetPos;
                        StopCoroutine(_playerMovementCor);
                        _playerMovementCor = null;
                        _playerMovementCor = StartCoroutine(PlayerMovement());
                    }
                    // geser ama play anim
                    //_playerTrans
                }
            }

            if (MobileSwipe.isReturnLeftSwipe())
            {
                playerPosition--;
                PlayerPosition--;
                Debug.Log("Left Swipe");
                if (PlayerPosition < PlayerPosition.LEFT)
                {
                    playerPosition = PlayerPosition.LEFT;
                    PlayerPosition = PlayerPosition.LEFT;
                }
                else
                {
                    if (_playerMovementCor == null)
                    {
                        Debug.LogWarning("Initiate Left Movement");
                        _playerMovementCor = StartCoroutine(PlayerMovement());
                    }
                    else
                    {
                        //_playerTrans.position = targetPos;
                        StopCoroutine(_playerMovementCor);
                        _playerMovementCor = null;
                        _playerMovementCor = StartCoroutine(PlayerMovement());
                    }
                    // geser ama play anim
                    //_playerTrans
                }
            }
        }
    }

    private IEnumerator PlayerMovement()
    {
        float startTime = Time.time;

        Vector3 startPos = _playerTrans.position;
        targetPos = pivotPlayerPos[(int)PlayerPosition].position;
        Debug.Log($"Vector: {startPos}, {targetPos}");
        float journalLenght = Vector3.Distance(startPos, targetPos);

        Debug.Log($"Jurnal Lenght: {journalLenght}");

        float fractionOfJourney = 0f;
        while (fractionOfJourney < 1f)
        {
            float distCovered = (Time.time - startTime) * speedAnimation;
            fractionOfJourney = distCovered / journalLenght;
            _playerTrans.position = Vector3.Lerp(startPos, targetPos, fractionOfJourney);
            Debug.Log(fractionOfJourney);
                
            yield return null;
        }
        _playerTrans.position = targetPos;

        _playerMovementCor = null;
    }

    private string text;
    GUIStyle gStyle;
    private void OnGUI()
    {
        if (gStyle == null)
        {
            gStyle = new GUIStyle();
            gStyle.fontSize = 60;
            gStyle.normal.textColor = Color.white;
        }

        float fps = 1.0f / deltaTime;

        text = $"FPS: {fps:.}\n" +
               $"Last input: NaN\n" +
               $"Current Position: {PlayerPosition}";
        GUI.TextArea(new Rect(30, 300, 400, 250), text, gStyle);
    }
}

public enum PlayerPosition
{
    LEFT,
    CENTER,
    RIGHT
}
