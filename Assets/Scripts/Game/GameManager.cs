using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
    {
    public int totalScore;
    public int nextOne;

    public List<MouseController> objectSelection;

    [SerializeField] private List<int> scoreValues;
    [SerializeField] private List<int> nextObject;
    [SerializeField] private List<int> currentObjects;

    public int listSize;
    public bool dropped;
    public static GameManager instance;

    [SerializeField] private float mouseCD = 0.5f;

    public bool canBeClicked = true;

    public TextMeshProUGUI score;
    public TextMeshProUGUI bestSc;

    public GameObject dangerText;
    public TextMeshProUGUI dangerTimer;
    public GameObject gameOverScreen;

    public bool isRunning = true;
    public bool timerRunning = false;
    public float endTimer = 5f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        listSize = objectSelection.Count;
        bestSc.text = SaveManager.GetBestScore().ToString();
    }

    private void Update()
    {
        if (isRunning)
        {
            score.text = totalScore.ToString();

            if (dropped)
            {
                if (currentObjects.Count < 2)
                {
                    int firstIndex = Random.Range(0, 6);
                    int secondIndex = firstIndex + 1;

                    PushL(nextObject[firstIndex]);
                    PushL(nextObject[secondIndex]);
                }

                int index = PopL();
                nextOne = currentObjects[0];

                MouseController selected = objectSelection[index];

                Instantiate(
                    selected,
                    selected.spawnPosition(selected.mousePos),
                    selected.transform.rotation
                );

                dropped = false;
                canBeClicked = false;
            }

            if (!canBeClicked)
            {
                mouseCD -= Time.deltaTime;

                if (mouseCD <= 0)
                {
                    canBeClicked = true;
                    mouseCD = 0.5f;
                }
            }

            if (timerRunning)
            {
                endTimer -= Time.deltaTime;

                dangerText.SetActive(true);
                dangerTimer.text = endTimer.ToString("F1");

                if (endTimer <= 0)
                {
                    isRunning = false;
                    endTimer = 0;
                    dangerTimer.text = endTimer.ToString("F1");
                    gameOverScreen.SetActive(true);
                }
            }
        }
        else
        {
            int best = SaveManager.GetBestScore();

            if (totalScore > best)
            {
                SaveManager.ResetBestScore();
                SaveManager.SaveBestScore(totalScore);
            }
        }
    }

    public void dropObject(MouseController mc)
    {
        mc.rb.bodyType = RigidbodyType2D.Dynamic;
        mc.rb.gravityScale = 1f;

        setMass(mc);

        mc.isBeingHeld = false;
        dropped = true;
    }

    public void evolution(int objID, Vector2 location)
    {
        MouseController newobj = Instantiate(
            objectSelection[objID + 1],
            location,
            default(Quaternion)
        );

        totalScore += scoreValues[objID + 1];

        newobj.isBeingHeld = false;
        newobj.rb.gravityScale = 1f;
        newobj.rb.bodyType = RigidbodyType2D.Dynamic;

        setMass(newobj);

        newobj.pc.enabled = true;
    }

    public void resetStats()
    {
        isRunning = true;
        totalScore = 0;
    }

    private void setMass(MouseController mc)
    {
        string tagObj = mc.gameObject.tag;

        switch (tagObj)
        {
            case "circle":
                mc.rb.mass = 1f;
                break;

            case "triangle":
                mc.rb.mass = 1f;
                break;

            case "square":
                mc.rb.mass = 1.4f;
                break;

            case "pill":
                mc.rb.mass = 1.6f;
                break;

            case "star":
                mc.rb.mass = 1.8f;
                break;

            case "badge":
                mc.rb.mass = 2f;
                break;

            case "uni":
                mc.rb.mass = 2.2f;
                break;

            case "pamela":
                mc.rb.mass = 2.4f;
                break;
        }
    }

    private int PopL()
    {
        int primero = currentObjects[0];

        currentObjects.RemoveAt(0);

        return primero;
    }

    private void PushL(int value)
    {
        currentObjects.Add(value);
    }
}
