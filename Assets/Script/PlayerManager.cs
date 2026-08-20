using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private Transform worldCam;
    [SerializeField] private Transform _player;
    [SerializeField] private Sensitivity currentSens = Sensitivity.NORMAL;
    private float _currentSpeedSens;
    private Vector3 _lastMousePos;

    public Transform WorldCam => worldCam;
    public Transform Player { get { return _player; } set { _player = value; } }
    public Sensitivity CurrentSens { get { return currentSens; } set { currentSens = value; } }
    public float CurrentSpeedSens { get { return _currentSpeedSens; } set { _currentSpeedSens = value; } }
    public Vector3 LastMousePos { get { return _lastMousePos; } set { _lastMousePos = value; } }

    private void Awake() { instance = this; }
}

public enum Sensitivity
{
    LOW,
    NORMAL,
    HIGH
}