using UnityEngine;

public class HomePageHUD : MonoBehaviour
{
    [SerializeField] private GameObject homeHUD;
    private void OnEnable()
    {
        GameManager.onGameStart += DisableHomeHUD;
    }

    private void OnDisable()
    {
        GameManager.onGameStart -= DisableHomeHUD;
    }

    private void DisableHomeHUD()
    {
        homeHUD.SetActive(false);
    }
}
