using UnityEngine;

public enum PuzzleType { None, Help, Match, Total }
public class PuzzleController : MonoBehaviour
{
    [SerializeField]
    Squid player, goal;

    [SerializeField]
    GameObject win, lose;

    [SerializeField]
    Prompt prompt;


    PuzzleType currentPuzzle = PuzzleType.None;
    bool isSolved = false;
    float delay = 7f;
    float solveTimer = 0f;

    int fails = 0;
    int gameOver = 5;

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
    }

    public void Solve()
    {
        isSolved = true;
        Debug.Log("YAY");
        win.SetActive(true);
        //GameController.Instance.Score++;
        //currentPuzzle = PuzzleType.None;
    }

    public void Fail()
    {
        fails++;
        if (fails >= gameOver)
        {
            //Display game over, reset button
        }

        isSolved = true;
        Debug.Log("noes");
        lose.SetActive(true);
        //currentPuzzle = PuzzleType.None;
    }

    public void ShowHideFishies(bool isOn)
    {
        fishies.SetActive(isOn);
    }

    public void StartMatchPuzzle()
    {
        if (isSolved)
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
            int rand = Random.Range(0, 7);

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
        if (isSolved)
            return;

        currentPuzzle = PuzzleType.Help;

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
                GameController.Instance.Score--;
                Fail();
                return;
            }  
        }
        Debug.Log("Match: Score + 1");
        GameController.Instance.Score++;
        Solve();
    }

    public void CheckHelp()
    {
        bool isRight = true;
        int puzzleScore = 0;
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

        Debug.Log("Score: " + puzzleScore);
        GameController.Instance.Score += puzzleScore;

        //if there is a timer, and it runs out, penalty of ?? points here?
        //if the player goes below a certain point value, game over?

        if (isRight)
            Solve();
        else
            Fail();

        fishies.SetActive(false);
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
                int rand = Random.Range(0, 1);

                if (rand == 0)
                    StartMatchPuzzle();
                else
                    StartHelpPuzzle();
            }
        }
    }
}