using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            GameManager.instance.GameEnd(GameEndCause.Border);
            GameManager.instance.EndLevel();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.GameEnd(GameEndCause.Obstacle);
            GameManager.instance.EndLevel();
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        
    //}
}
