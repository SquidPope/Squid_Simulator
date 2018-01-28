using UnityEngine;

public enum PuzzleType { None, Help, Match, Total }
public class PuzzleController : MonoBehaviour
{
    [SerializeField]
    Squid player, goal;

    [SerializeField]
    GameObject win, lose, gameOverText, gameWinText;

    [SerializeField]
    Prompt prompt;


    PuzzleType currentPuzzle = PuzzleType.None;
    bool isSolved = false;
    float delay = 5f;
    float solveTimer = 0f;

    int totalPuzzles = 0;
    int puzzlesToWin = 7;
    bool hasWon = false;

    int gameOver = -5;
    bool hasLost = false;

    [SerializeField]
    GameObject fishies;

    [SerializeField]
    Fish topFish, leftFish, rightFish;

    private static PuzzleController instance;
    public static PuzzleController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("PuzzleController");
                instance = go.GetComponent<PuzzleController>();
            }

            return instance;
        }
    }

    public PuzzleType GetCurrentPuzzleType()
    {
        return currentPuzzle;
    }

    public void Start()
    {
        win.SetActive(false);
        lose.SetActive(false);
        gameOverText.SetActive(false);
        gameWinText.SetActive(false);
    }

    public bool GetIsSolved()
    {
        return isSolved;
    }

    public void Solve()
    {
        isSolved = true;
        win.SetActive(true);

        totalPuzzles++;
        if (totalPuzzles >= puzzlesToWin)
        {
            gameWinText.SetActive(true);
            goal.gameObject.SetActive(false);
            hasWon = true;
            GameController.Instance.State = GameState.GameOver;
        }
    }

    public void Fail()
    {
        if (GameController.Instance.Score <= gameOver)
        {
            //Display game over, reset button
            gameOverText.SetActive(true);
            goal.gameObject.SetActive(false);
            Camera.main.backgroundColor = Color.black;
            hasLost = true;
            GameController.Instance.State = GameState.GameOver;
        }

        totalPuzzles++;
        if (totalPuzzles >= puzzlesToWin)
        {
            gameWinText.SetActive(true);
            goal.gameObject.SetActive(false);
            hasWon = true;
            GameController.Instance.State = GameState.GameOver;
        }

        isSolved = true;
        lose.SetActive(true);
    }

    public void ShowHideFishies(bool isOn)
    {
        fishies.SetActive(isOn);
    }

    public void StartMatchPuzzle()
    {
        if (isSolved || hasLost || hasWon)
            return;

        //Randomize goal colors
        for (int i = 0; i < (int)SquidPartType.Total; i++)
        {
            SquidPartType t = (SquidPartType)i;

            if (goal.GetPartColor((SquidPartType)i) == Color.clear)
            {
                Debug.Log("goal doesn't contain part of type: " + (SquidPartType)i);
                continue;
            }
            int rand = Random.Range(0, 8);

            switch (rand)
            {
                case 0:
                    goal.SetPartColor((SquidPartType)i, Color.grey);
                    break;
                case 1:
                    goal.SetPartColor((SquidPartType)i, Color.blue);
                    break;
                case 2:
                    goal.SetPartColor((SquidPartType)i, Color.cyan);
                    break;
                case 3:
                    goal.SetPartColor((SquidPartType)i, Color.green);
                    break;
                case 4:
                    goal.SetPartColor((SquidPartType)i, Color.yellow);
                    break;
                case 5:
                    goal.SetPartColor((SquidPartType)i, Color.red);
                    break;
                case 6:
                    goal.SetPartColor((SquidPartType)i, Color.magenta);
                    break;
                case 7:
                    goal.SetPartColor((SquidPartType)i, Color.white);
                    break;
            }

            currentPuzzle = PuzzleType.Match;

            prompt.gameObject.SetActive(true);
            prompt.Show();
        }
    }

    public void StartHelpPuzzle()
    {
        if (isSolved || hasLost || hasWon)
            return;

        currentPuzzle = PuzzleType.Help;
        goal.SetPartColor(SquidPartType.Left, Color.white);
        goal.SetPartColor(SquidPartType.Right, Color.white);
        goal.SetPartColor(SquidPartType.Top, Color.white);

        for (int i = 0; i < (int)SquidPartType.Total; i++)
        {
            goal.SetPartColor((SquidPartType)i, Color.grey);
        }

        fishies.SetActive(true);

        topFish.Randomize();
        leftFish.Randomize();
        rightFish.Randomize();

        prompt.gameObject.SetActive(true);
        prompt.Show();
    }

    public void CheckForMatch()
    {
        for (int i = 0; i < (int)SquidPartType.Total; i++)
        {
            if (player.GetPartColor((SquidPartType)i) != goal.GetPartColor((SquidPartType)i))
            {
                GameController.Instance.Score -= 2;
                Fail();
                return;
            }  
        }

        GameController.Instance.Score += 2;
        Solve();
    }

    public void CheckHelp()
    {
        bool isRight = true;
        int puzzleScore = 0;

        //Color the goal squid so players can see what the correct solution was.
        goal.SetPartColor(SquidPartType.Top, topFish.correctColor);
        goal.SetPartColor(SquidPartType.Left, leftFish.correctColor);
        goal.SetPartColor(SquidPartType.Right, rightFish.correctColor);

        //check each player part against correct answer.
        if (player.GetPartColor(SquidPartType.Top) == topFish.correctColor)
        {
            puzzleScore += topFish.correctValue;
        }
        else
        {
            puzzleScore += topFish.wrongValue;
            isRight = false;
        }

        if (player.GetPartColor(SquidPartType.Left) == leftFish.correctColor)
        {
            puzzleScore += leftFish.correctValue;
        }
        else
        {
            puzzleScore += leftFish.wrongValue;
            isRight = false;
        }

        if (player.GetPartColor(SquidPartType.Right) == rightFish.correctColor)
        {
            puzzleScore += rightFish.correctValue;
        }
        else
        {
            puzzleScore += rightFish.wrongValue;
            isRight = false;
        }

        GameController.Instance.Score += puzzleScore;

        //if there is a timer, and it runs out, penalty of ?? points here?
        //if the player goes below a certain point value, game over?

        if (isRight)
            Solve();
        else
            Fail();

        fishies.SetActive(false);
    }

    public void StartPuzzle()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            StartHelpPuzzle();
        else
            StartMatchPuzzle();
    }

    private void Update()
    {
        if  (isSolved)
        {
            solveTimer += Time.deltaTime;
            if (solveTimer >= delay)
            {
                isSolved = false;
                win.SetActive(false);
                lose.SetActive(false);
                currentPuzzle = PuzzleType.None;

                solveTimer = 0;

                //ToDo: randomize type of puzzle.
                int rand = Random.Range(0, 2);

                if (rand == 0)
                    StartMatchPuzzle();
                else
                    StartHelpPuzzle();
            }
        }
    }
}