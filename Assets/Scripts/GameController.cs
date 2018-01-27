using UnityEngine;

public enum GameState { Brain, MainMenu, Squid }
public class GameController : MonoBehaviour
{

    GameState state;
    int neuronLimit = 6;
    public int totalNeurons = 0;
    public int score = 0;

    public GameState State
    {
        get { return state; }
        set
        {
            state = value;
            UIController.Instance.UpdateUI();
        }
    }

    public int NeuronLimit { get { return neuronLimit; } }

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
