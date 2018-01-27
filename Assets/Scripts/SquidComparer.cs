using UnityEngine;

public enum PuzzleType { Help, Match }
public class PuzzleController : MonoBehaviour
{

    [SerializeField]
    Squid a, b;

    PuzzleType currentPuzzle = PuzzleType.Match; //ToDo: randomize on start when both types are confirmed.



    bool CheckForMatch()
    {
        for (int i = 0; i < (int)SquidPartType.Total; i++)
        {
            if (a.GetPartColor((SquidPartType)i) != b.GetPartColor((SquidPartType)i))
                return false;
        }

        return true;
    }
}
