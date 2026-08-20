using UnityEngine;

public class PlayerDragMovement : MonoBehaviour
{
    [SerializeField] private float margin = 100;
    //private PlayerManager _playerManager;

    private void Start()
    {
        //_playerManager = PlayerManager.instance;
    }

    void Update()
    {
        if (GameManager.instance.GameState != GameState.Playing) return;

        PlayerManager _playerManager = PlayerManager.instance;

        if (Input.GetMouseButtonDown(0))
            _playerManager.LastMousePos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePos = Input.mousePosition;
            if (currentMousePos != _playerManager.LastMousePos)
            {
                Debug.Log(currentMousePos);
                _playerManager.Player.position += new Vector3(
                    (currentMousePos.x - _playerManager.LastMousePos.x) / margin * _playerManager.CurrentSpeedSens,
                    (currentMousePos.y - _playerManager.LastMousePos.y) / margin * _playerManager.CurrentSpeedSens,
                    0f);
            }

            _playerManager.LastMousePos = currentMousePos;
        }
    }

    public void ResetPos() => PlayerManager.instance.Player.position = new Vector3(0f, 1.3f, 0f);

    private void OnEnable()
    {
        GameManager.instance.onResetToMainMenu += ResetPos;
    }

    private void OnDisable()
    {
        GameManager.instance.onResetToMainMenu -= ResetPos;
    }
}
