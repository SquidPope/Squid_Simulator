using UnityEngine;

public enum PuzzleType { Help, Match }
public class PuzzleController : MonoBehaviour
{
    [SerializeField]
    Squid player, goal;

    PuzzleType currentPuzzle = PuzzleType.Match; //ToDo: randomize on start when both types are confirmed.

    public void StartMatchPuzzle()
    {
        //Randomize goal colors
        for (int i = 0; i < (int)SquidPartType.Total; i++)
        {
            if (goal.GetPartColor((SquidPartType)i) == Color.clear)
            {
                Debug.Log("goal doesn't contain part of type: " + (SquidPartType)i);
                continue;
            }
            int rand = Random.Range(0, 7);

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
                default:
                    goal.SetPartColor((SquidPartType)i, Color.black);
                    break;
            }
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
}