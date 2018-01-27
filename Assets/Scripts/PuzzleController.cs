using UnityEngine;

public enum PuzzleType { None, Help, Match, Total }
public class PuzzleController : MonoBehaviour
{
    [SerializeField]
    Squid player, goal;

    PuzzleType currentPuzzle = PuzzleType.None;
    bool isSolved = false;
    float delay = 5f;
    float solveTimer = 0f;

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

    public void Solve()
    {
        isSolved = true;
        currentPuzzle = PuzzleType.None;
        GameController.Instance.score++;
    }

    public void StartMatchPuzzle()
    {
        //Randomize goal colors
        for (int i = 0; i < (int)SquidPartType.Total; i++)
        {
            SquidPartType t = (SquidPartType)i;
            Debug.Log("goal part color: " + goal.GetPartColor((SquidPartType)i));

            if (goal.GetPartColor((SquidPartType)i) == Color.clear)
            {
                Debug.Log("goal doesn't contain part of type: " + (SquidPartType)i);
                continue;
            }
            int rand = Random.Range(0, 7);
            Debug.Log("rand: " + rand);

            switch (rand)
            {
                case 0:
                    goal.SetPartColor((SquidPartType)i, Color.black);
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
        }
    }

    bool CheckForMatch()
    {
        for (int i = 0; i < (int)SquidPartType.Total; i++)
        {
            if (player.GetPartColor((SquidPartType)i) != goal.GetPartColor((SquidPartType)i))
                return false;
        }

        return true;
    }

    private void Update()
    {
        if  (isSolved)
        {
            solveTimer += Time.deltaTime;
            if (solveTimer >= delay)
            {
                isSolved = false;

                //ToDo: randomize type of puzzle.
                StartMatchPuzzle();
            }
        }
    }
}