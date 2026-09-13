using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameObject gameOverScreen;
    public bool isBeingTouched = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.isRunning = false;
        gameOverScreen.SetActive(true);
    }

}
