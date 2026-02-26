using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("Level1");
    }
}
