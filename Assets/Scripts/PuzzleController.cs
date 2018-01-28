using UnityEngine;

public enum PuzzleType { None, Help, Match, Total }
public class PuzzleController : MonoBehaviour
{
    [SerializeField]
    Squid player, goal;

    [SerializeField]
    GameObject win;

    [SerializeField]
    Prompt prompt;


    PuzzleType currentPuzzle = PuzzleType.None;
    bool isSolved = false;
    float delay = 5f;
    float solveTimer = 0f;

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
    }

    public void Solve()
    {
        isSolved = true;
        Debug.Log("YAY");
        win.SetActive(true);
        GameController.Instance.Score++;
        currentPuzzle = PuzzleType.None;
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

        //Get correct red/green/blue value for top, left, and right
        //Start a timer?
    }

    public void CheckForMatch()
    {
        for (int i = 0; i < (int)SquidPartType.Total; i++)
        {
            if (player.GetPartColor((SquidPartType)i) != goal.GetPartColor((SquidPartType)i))
                return;
        }

        Solve();
        
    }

    public void CheckHelp()
    {
        //check each player part against correct answer.
        if (player.GetPartColor(SquidPartType.Top) == topFish.correctColor)
        {
            GameController.Instance.Score += topFish.correctValue;
        }
        else
        {
            GameController.Instance.Score += topFish.wrongValue;
        }

        if (player.GetPartColor(SquidPartType.Left) == leftFish.correctColor)
        {
            GameController.Instance.Score += leftFish.correctValue;
        }
        else
        {
            GameController.Instance.Score += leftFish.wrongValue;
        }

        if (player.GetPartColor(SquidPartType.Right) == rightFish.correctColor)
        {
            GameController.Instance.Score += rightFish.correctValue;
        }
        else
        {
            GameController.Instance.Score += rightFish.wrongValue;
        }

        //if there is a timer, and it runs out, penalty of ?? points here?
        //if the player goes below a certain point value, game over?

        Solve();
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