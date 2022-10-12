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
    public GameObject startButton;
    public GameObject redButton;
    public GameObject greenButton;
    public GameObject blueButton;
    public bool isLevelOneComplete = false;
    public bool isListCleared = false;
    public bool isLevelTwoComplete = false;
    public bool isLevelThreeComplete = false;
    public int puzzleIndex; // 0 = none complted | 1 = level 1 completed | 2 

    public List<ColorPlate> plates;

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

    // Start is called before the first frame update
    void Start()
    {
        winObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //level one
        levelOneUpdate();

        //level2
        if(isLevelOneComplete)
        {
            levelTwoUpdate();
        }

        //level3
        if (isLevelTwoComplete)
        {
            levelThreeUpdate();
        }

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
        if (index == 1.0f)
        {
            plates[0].ActivatePlate();
        }
        if (index == 2.0f)
        {
            plates[1].ActivatePlate();
        }
        if (index == 3.0f)
        {
            plates[2].ActivatePlate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("redButton"))
        {
            Debug.Log("clicked red button");
            evnt_red.Post(gameObject);
            levelOneTestArray.Add(2.0f);
            levelOneCount++;
            if(isLevelOneComplete)
            {
                levelTwoTestArray.Add(2.0f);
                levelTwoCount++;
            }
            if (isLevelTwoComplete)
            {
                levelThreeTestArray.Add(2.0f);
                levelThreeCount++;
            }
        }
        else if (other.gameObject.CompareTag("greenButton"))
        {
            Debug.Log("clicked green button");
            evnt_green.Post(gameObject);
            levelOneTestArray.Add(0.0f);
            levelOneCount++;
            if (isLevelOneComplete)
            {
                levelTwoTestArray.Add(0.0f);
                levelTwoCount++;
            }
            if (isLevelTwoComplete)
            {
                levelThreeTestArray.Add(0.0f);
                levelThreeCount++;
            }
        }
        else if (other.gameObject.CompareTag("blueButton"))
        {
            Debug.Log("clicked blue button");
            evnt_blue.Post(gameObject);
            levelOneTestArray.Add(1.0f);         
            levelOneCount++;
            if (isLevelOneComplete)
            {
                levelTwoTestArray.Add(1.0f);
                levelTwoCount++;
            }
            if (isLevelTwoComplete)
            {
                levelThreeTestArray.Add(1.0f);
                levelThreeCount++;
            }
        }

    }

    public void buttonPress()
    {
        Debug.Log("started game");
        if(isLevelOneComplete == false)
        {
            levelOne();
        }
        if(isLevelOneComplete && isLevelTwoComplete == false)
        {
            levelTwo();
        }
        if(isLevelTwoComplete && isLevelThreeComplete == false)
        {
            levelThree();
        }
        if(isLevelThreeComplete)
        {
            winObject.SetActive(true);
            startButton.SetActive(false);
            redButton.SetActive(false);
            blueButton.SetActive(false);
            greenButton.SetActive(false);
        }
    }

    void levelOne()
    {
        Debug.Log("ENTERED LEVEL 1");
        evnt_puzzleOneSequence.Post(gameObject);
        levelOneCount = 0;
        levelOneTestArray.Clear();
    }

    void levelOneUpdate()
    {
        if (levelOneCount == 3 && isListSame(levelOneArray, levelOneTestArray))
        {
            isLevelOneComplete = true;
        }
        if (levelOneTestArray.Count > 3)
        {
            levelOneTestArray.Clear();
            isListCleared = true;
            levelOneCount = 0;
        }
        if (isLevelOneComplete == true)
        {
        }
    }

    public void CheckPuzzleState()
    {
        // Checks puzzle index
        // Depending on the puzzle index, compares the current players moves to the correct array's count
        // check if current players moves match the correct array
    }

    void levelTwo()
    {
        Debug.Log("ENTERED LEVEL 2");
        evnt_puzzleTwoSequence.Post(gameObject);
        levelTwoCount = 0;
        levelTwoTestArray.Clear();
    }

    void levelTwoUpdate()
    {
        if (levelTwoCount == 5 && isListSame(levelTwoArray, levelTwoTestArray) && isLevelOneComplete)
        {
            isLevelTwoComplete = true;
        }
        if (levelTwoTestArray.Count > 5)
        {
            levelTwoTestArray.Clear();
            isListCleared = true;
            levelTwoCount = 0;
        }
        if (isLevelTwoComplete == true)
        {
        }
    }


    void levelThree()
    {
        Debug.Log("ENTERED LEVEL 3");
        evnt_puzzleThreeSequence.Post(gameObject);
        levelThreeCount = 0;
        levelThreeTestArray.Clear();
        if (isLevelThreeComplete)
        {
            Debug.Log("finished level 3");
        }
    }

    void levelThreeUpdate()
    {
        if (levelThreeCount == 7 && isListSame(levelThreeArray, levelThreeTestArray) && isLevelTwoComplete)
        {
            isLevelThreeComplete = true;
        }
        if (levelThreeTestArray.Count > 7)
        {
            levelThreeTestArray.Clear();
            levelThreeCount = 0;
        }
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
