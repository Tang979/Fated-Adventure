using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void btnStart()
    {
        SceneManager.LoadScene("Game");
    }
}
