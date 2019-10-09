using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<Button> btns = new List<Button>();
    public Sprite image;
    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();

    public int nextScene;

    public Text uiText;
    public Text healthTxt;
    public float availableTime;

    private float seconds;
    private float minutes;
    private float gameTime;

    private string remainingTime;

    private bool firstGuess;
    private bool secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex;
    private int secondGuessIndex;

    private string firstGuessPuzzle;
    private string secondGuessPuzzle;

    // health
    public int health = 3;


    private void Awake()
    {
        //puzzles = Resources.LoadAll<Sprite>("Sprites");    
    }

    private void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
        healthTxt.text = health.ToString();

    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = image;
        }
    }

    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    public void PickAPuzzle()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;



        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            btns[firstGuessIndex].interactable = false;

        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            btns[secondGuessIndex].interactable = false;
            countGuesses++;

            if (gamePuzzles[firstGuessIndex].name == "axe-card" || gamePuzzles[secondGuessIndex].name == "axe-card")
            {
                health--;
                healthTxt.text = health.ToString();
                if (gamePuzzles[firstGuessIndex].name == "axe-card")
                    btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);

                if (gamePuzzles[secondGuessIndex].name == "axe-card")
                    btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            }

            if (firstGuessPuzzle != secondGuessPuzzle)
            {
                
                btns[firstGuessIndex].interactable = true;
                btns[secondGuessIndex].interactable = true;
            }

            

            StartCoroutine(CheckIfThePuzzlesMatch());
        }
    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(.5f);
            // cant touch buttons
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
            /*
            if (gamePuzzles[firstGuessIndex].name == "axe-card" && gamePuzzles[secondGuessIndex].name == "axe-card")
            {
                if (health == 1)
                {
                    SceneManager.LoadSceneAsync("Gameover");
                }
                health--;
                healthTxt.text = health.ToString();
            }
            */
            // color goes transparent, can comment out if still want to see them
          //  btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            //btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
            CheckIfTheGameIsFinished();
        }

        else
        {
            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].image.sprite = image;
            btns[secondGuessIndex].image.sprite = image;

        }

        yield return new WaitForSeconds(.5f);

        firstGuess = false;
        secondGuess = false;

    }

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;
        if (countCorrectGuesses == gameGuesses)
        {
            print("finished");
            StartCoroutine(NextLevel());
        }

        // if the only buttons left are axe buttons, then end game

    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextScene);
    }

    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void Update()
    {
        gameTime = (int)(availableTime - Time.timeSinceLevelLoad);
        seconds = Mathf.CeilToInt(availableTime - Time.timeSinceLevelLoad) % 60;
        minutes = Mathf.CeilToInt(availableTime - Time.timeSinceLevelLoad) / 60;
        remainingTime = string.Format( "{0:00} : {1:00}", minutes, seconds);

        uiText.text = remainingTime;

        if (gameTime <= 0)
        {
            SceneManager.LoadSceneAsync("Gameover");
        }
    }
}
