using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour
{
    public void GoToScene(string target)
    {
        SceneManager.LoadScene(target);
    }
    public void QuitApp()
    {
        Application.Quit();
    }
}
