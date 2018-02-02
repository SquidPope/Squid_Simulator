using UnityEngine;

public enum GameState { Brain, GameOver, LevelEditor, MainMenu, Squid }
public class GameController : MonoBehaviour
{
    GameState state;
    int neuronLimit = 9;
    public int totalNeurons = 0;
    public int score = 0; //ToDo: count # of nodes selected per puzzle, reward fewer.

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

    public GameState State
    {
        get { return state; }
        set
        {
            state = value;
            UIController.Instance.UpdateUI();

            if (state == GameState.Squid && PuzzleController.Instance.GetCurrentPuzzleType() == PuzzleType.Match)
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

    private void Start()
    {

        State = GameState.MainMenu;
        DontDestroyOnLoad(gameObject);
    }

}
