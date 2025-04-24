using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Match this exactly to your scene name
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting..."); // Only shows in editor
    }
}
