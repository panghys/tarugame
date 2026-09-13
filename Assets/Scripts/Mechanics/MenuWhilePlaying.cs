using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuWhilePlaying : MonoBehaviour
{

    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.instance.resetStats();
    }
}
