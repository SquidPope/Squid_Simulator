using UnityEngine;

public enum GameState { Brain, MainMenu, Squid }
public class GameController : MonoBehaviour
{
    GameState state;
    int neuronLimit = 6;
    public int totalNeurons = 0;
    public int score = 0; //ToDo: count # of nodes selected per puzzle, reward fewer.

    public GameState State
    {
        get { return state; }
        set
        {
            state = value;
            UIController.Instance.UpdateUI();

            if (state == GameState.Squid && PuzzleController.Instance.GetCurrentPuzzleType() == PuzzleType.None)
            {
                //ToDo: randomize
                int rand = Random.Range(0, 1);
                if (rand == 0)
                    PuzzleController.Instance.StartHelpPuzzle();
                else
                    PuzzleController.Instance.StartMatchPuzzle();
            }
            else if (state == GameState.Squid && PuzzleController.Instance.GetCurrentPuzzleType() == PuzzleType.Match)
            {
                PuzzleController.Instance.CheckForMatch();
            }
            else if (state == GameState.Squid && PuzzleController.Instance.GetCurrentPuzzleType() == PuzzleType.Help)
            {
                PuzzleController.Instance.CheckHelp();
            }
        }
    }

    public int NeuronLimit { get { return neuronLimit; } }

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            UIController.Instance.UpdateScore();
        }
    }

    static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("GameController");
                instance = go.GetComponent<GameController>();
            }

            return instance;
        }
    }

    private void Start()
    {

        State = GameState.MainMenu;
    }

}
