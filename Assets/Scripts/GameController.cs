using UnityEngine;

public enum GameState { Brain, Squid }
public class GameController : MonoBehaviour
{
    GameState state;
    int neuronLimit = 3;
    public int totalNeurons = 0;

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
        state = GameState.Brain;
    }

}
