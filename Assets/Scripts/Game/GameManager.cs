using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class GameManager : MonoBehaviour
{
    public int totalScore = 0;
    public int nextOne;
    public List<MouseController> objectSelection;
    [SerializeField] private List<int> scoreValues;
    [SerializeField] private List<int> nextObject;
    [SerializeField] private List<int> currentObjects;
    public int listSize;
    public bool dropped = false;
    public static GameManager instance;
    [SerializeField] private float mouseCD = 0.5f;
    public bool canBeClicked = true;
    public TextMeshProUGUI score;
    public TextMeshProUGUI bestSc;
    public bool isRunning = true;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        listSize = objectSelection.Count;
        bestSc.text = (SaveManager.GetBestScore()).ToString();

    }

    void Update()
    {
        if (isRunning)
        {
            score.text = totalScore.ToString();
            if (dropped)
            {
                if(currentObjects.Count < 2)
                {
                    int firstIndex = UnityEngine.Random.Range(0,6);
                    int secondIndex = firstIndex+1;
                    PushL(nextObject[firstIndex]);
                    PushL(nextObject[secondIndex]);
                }
                int index = PopL(); nextOne = currentObjects[0];
                MouseController selected = objectSelection[index];
                Instantiate(selected,selected.spawnPosition(selected.mousePos),selected.transform.rotation);
                dropped = false;
                canBeClicked = false;
            }
            if (!canBeClicked)
            {
                mouseCD -= Time.deltaTime;
                if(mouseCD <= 0)
                {
                    canBeClicked = true;
                    mouseCD = 0.5f;
                }
            }
        }
        else
        {
            int best = SaveManager.GetBestScore();
            if(totalScore > best)
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
        MouseController newobj = Instantiate(objectSelection[objID+1],location,default(Quaternion));
        totalScore += scoreValues[objID+1];
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
            case("circle"):
                mc.rb.mass = 1;
                break;
            case("triangle"):
                mc.rb.mass = 1;
                break;
            case("square"):
                mc.rb.mass = 1.4f;
                break;
            case("pill"):
                mc.rb.mass = 1.6f;
                break;
            case("star"):
                mc.rb.mass = 1.8f;
                break;
            case("badge"):
                mc.rb.mass = 2f;
                break;
            case("uni"):
                mc.rb.mass = 2.2f;
                break;
            case("pamela"):
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


