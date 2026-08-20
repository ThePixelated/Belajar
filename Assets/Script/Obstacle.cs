using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speedMovement;
    //[SerializeField] private DirectionBool direction;
    [SerializeField] private Vector2 direction;

    private Vector3 _originSpawnPoint;
    //public Vector3 OriginSpawnPoint { get; private set; }

    private void Awake()
    {
        _originSpawnPoint = transform.position;

        RandomizeDIrection();
    }

    private void Update()
    {
        if (GameManager.instance.GameState == GameState.Playing)
        {
            transform.position += (Vector3)direction * speedMovement * Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        GameManager.instance.onResetToMainMenu += ResetPosObstacle;
    }

    private void OnDisable()
    {
        GameManager.instance.onResetToMainMenu -= ResetPosObstacle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "UpperBorder":
                direction.y = -1;
                break;
            case "LowerBorder":
                direction.y = 1;
                break;
            case "LeftBorder":
                direction.x = 1;
                break;
            case "RightBorder":
                direction.x = -1;
                break;
        }
    }

    private void RandomizeDIrection()
    {
        int randXVal = Random.Range(0, 2);
        if (randXVal == 0)
            direction.x = -1;
        else
            direction.x = 1;

        int randYVal = Random.Range(0, 2);
        if (randYVal == 0)
            direction.y = -1;
        else
            direction.y = 1;
    }

    public void ResetPosObstacle() { transform.position = _originSpawnPoint; RandomizeDIrection(); /*Debug.Log("RESET OBS POS!");*/ }
}

//[System.Serializable]
//public class DirectionBool
//{
//    public bool IsForward;
//    public bool IsUpward;
//}
