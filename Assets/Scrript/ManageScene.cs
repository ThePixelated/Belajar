using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    public void GoToTargetScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
