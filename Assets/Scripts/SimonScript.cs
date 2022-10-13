using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimonScript : MonoBehaviour
{
    //0 = green, 1 = blue, 2 = red
    public AK.Wwise.Event evnt_red = null;
    public AK.Wwise.Event evnt_green = null;
    public AK.Wwise.Event evnt_blue = null;
    public AK.Wwise.Event evnt_puzzleOneSequence = null;
    public AK.Wwise.Event evnt_puzzleTwoSequence = null;
    public AK.Wwise.Event evnt_puzzleThreeSequence = null;
    public ColorPlate redPlate;
    public ColorPlate greenPlate;
    public ColorPlate bluePlate;
    public GameObject startButton;
    public GameObject redButton;
    public GameObject greenButton;
    public GameObject blueButton;
    public bool isLevelOneComplete = false;
    //public bool isLevelTwoComplete = false;
    //public bool isLevelThreeComplete = false;
    public int puzzleIndex; // 0 = none complted | 1 = level 1 completed | 2 

    public List<float> levelOneArray = new List<float>{0.0f, 1.0f, 2.0f};
    public List<float> levelOneTestArray = new();
    public List<float> levelTwoArray = new List<float> { 0.0f, 0.0f, 2.0f, 1.0f, 0.0f };
    public List<float> levelTwoTestArray = new();
    public List<float> levelThreeArray = new List<float> {0.0f, 2.0f, 1.0f, 2.0f, 0.0f, 1.0f, 2.0f};
    public List<float> levelThreeTestArray = new();
    public float levelOneCount;
    public float levelTwoCount;
    public float levelThreeCount;
    public GameObject winObject;
    public GameObject levelOnePrint;
    public GameObject levelTwoPrint;
    public GameObject levelThreePrint;

    // Start is called before the first frame update
    void Start()
    {
        winObject.SetActive(false);
        levelOnePrint.SetActive(false);
        levelTwoPrint.SetActive(false);
        levelThreePrint.SetActive(false);
    }


    public IEnumerator PlayLevel(int index)
    {
        switch (index) {

            case 1:
                foreach(float f in levelOneArray)
                {
                    ActivateSpecificPlate(f);
                    yield return new WaitForSeconds(1f);
                }
                break;
            case 2:
                foreach (float f in levelTwoArray)
                {
                    ActivateSpecificPlate(f);
                    yield return new WaitForSeconds(1f);
                }
                break;
            case 3:
                foreach (float f in levelThreeArray)
                {
                    ActivateSpecificPlate(f);
                    yield return new WaitForSeconds(1f);
                }
                break;
            default:
                break;
        
        }
    }

    public void ActivateSpecificPlate(float index)
    {
        if (index == 0.0f)
        {
            greenPlate.ActivatePlate();
        }
        if (index == 1.0f)
        {
            bluePlate.ActivatePlate();
        }
        if (index == 2.0f)
        {
            //redPlate.ActivatePlate();
            redButton.GetComponent<ColorPlate>().ActivatePlate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("redButton"))
        {
            CheckPuzzleState();
            Debug.Log("clicked red button");
            //evnt_red.Post(gameObject);
            // button glow
            redButton.GetComponent<ColorPlate>().ActivatePlate();
            levelOneTestArray.Add(2.0f);
            levelOneCount++;
            if(puzzleIndex == 1)
            {
                levelTwoTestArray.Add(2.0f);
                levelTwoCount++;
            }
            if (puzzleIndex == 2)
            {
                levelThreeTestArray.Add(2.0f);
                levelThreeCount++;
            }
        }
        else if (other.gameObject.CompareTag("greenButton"))
        {
            CheckPuzzleState();
            Debug.Log("clicked green button");
            //evnt_green.Post(gameObject);
            greenButton.GetComponent<ColorPlate>().ActivatePlate();
            levelOneTestArray.Add(0.0f);
            levelOneCount++;
            if (puzzleIndex == 1)
            {
                levelTwoTestArray.Add(0.0f);
                levelTwoCount++;
            }
            if (puzzleIndex == 2)
            {
                levelThreeTestArray.Add(0.0f);
                levelThreeCount++;
            }
        }
        else if (other.gameObject.CompareTag("blueButton"))
        {
            CheckPuzzleState();
            Debug.Log("clicked blue button");
            blueButton.GetComponent<ColorPlate>().ActivatePlate();
            //evnt_blue.Post(gameObject);
            levelOneTestArray.Add(1.0f);         
            levelOneCount++;
            if (puzzleIndex == 1)
            {
                levelTwoTestArray.Add(1.0f);
                levelTwoCount++;
            }
            if (puzzleIndex == 2)
            {
                levelThreeTestArray.Add(1.0f);
                levelThreeCount++;
            }
        }

    }

    public void buttonPress()
    {
        CheckPuzzleState();
        if (puzzleIndex == 0)
        {
            levelOnePrint.SetActive(true);
            StartCoroutine(PlayLevel(1));
            levelOne();
        }
        if(puzzleIndex == 1)
        {
            levelOnePrint.SetActive(false);
            levelTwoPrint.SetActive(true);
            StartCoroutine(PlayLevel(2));
            levelTwo();
        }
        if(puzzleIndex == 2)
        {
            levelTwoPrint.SetActive(false);
            levelThreePrint.SetActive(true);
            StartCoroutine(PlayLevel(3));
            levelThree();
        }
        if(puzzleIndex == 3)
        {
            levelThreePrint.SetActive(false);
            winObject.SetActive(true);
            startButton.SetActive(false);
            redButton.SetActive(false);
            blueButton.SetActive(false);
            greenButton.SetActive(false);
        }
    }

    void levelOne()
    {
        //evnt_puzzleOneSequence.Post(gameObject);
        levelOneCount = 0;
        levelOneTestArray.Clear();
    }


    public void CheckPuzzleState()
    {
        Debug.Log(puzzleIndex);
        if(puzzleIndex == 0)
        {
            if (levelOneCount == 3 && isListSame(levelOneArray, levelOneTestArray))
            {
                puzzleIndex = 1;
            }
            if (levelOneTestArray.Count > 3)
            {
                levelOneTestArray.Clear();
                levelOneCount = 0;
            }
        }
        if(puzzleIndex == 1)
        {
            if (levelTwoCount == 5 && isListSame(levelTwoArray, levelTwoTestArray))
            {
                puzzleIndex = 2;
            }
            if (levelTwoTestArray.Count > 5)
            {
                levelTwoTestArray.Clear();
                levelTwoCount = 0;
            }
        }
        if(puzzleIndex == 2)
        {
            if (levelThreeCount == 7 && isListSame(levelThreeArray, levelThreeTestArray))
            {
                puzzleIndex = 3;
            }
            if (levelThreeTestArray.Count > 7)
            {
                levelThreeTestArray.Clear();
                levelThreeCount = 0;
            }
        }
    }

    void levelTwo()
    {
        Debug.Log("ENTERED LEVEL 2");
        //evnt_puzzleTwoSequence.Post(gameObject);
        levelTwoCount = 0;
        levelTwoTestArray.Clear();
    }


    void levelThree()
    {
        Debug.Log("ENTERED LEVEL 3");
        //evnt_puzzleThreeSequence.Post(gameObject);
        levelThreeCount = 0;
        levelThreeTestArray.Clear();
    }

    bool isListSame(List<float> a, List<float> b)
    {
        if(a.Count != b.Count)
        {
            return false;
        }
        for (var i = 0; i < a.Count; i++)
        {
            if (b[i] != a[i])
            {
                return false;
            }
        }
        return true;
    }

}
