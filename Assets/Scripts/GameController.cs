using UnityEngine;

public enum GameState { Brain, Squid }
public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject brainStateObject, squidStateObject;


    GameState state;
    int neuronLimit = 6;
    public int totalNeurons = 0;

    public GameState State
    {
        get { return state; }
        set
        {
            state = value;
            if (state == GameState.Brain)
            {
                brainStateObject.SetActive(true);
                squidStateObject.SetActive(false);
            }
            else if (state == GameState.Squid)
            {
                brainStateObject.SetActive(false);
                squidStateObject.SetActive(true);
            }
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

        State = GameState.Brain;
    }

}
