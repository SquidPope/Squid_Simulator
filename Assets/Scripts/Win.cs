using UnityEngine;

public class Win : MonoBehaviour
{
    ParticleSystem ps;
    float delay;
    float delayTimer;

    private void Update()
    {
        delayTimer += Time.deltaTime;
        if (delayTimer >= delay)
        {
            delayTimer = 0f;
            ps.startColor = RandomColor();
            ps.Emit(10);
        }
    }

    private Color RandomColor()
    {
        int rand = Random.Range(0, 6);
        if (rand == 0)
            return Color.red;
        else if (rand == 1)
            return Color.green;
        else if (rand == 2)
            return Color.blue;
        else if (rand == 3)
            return Color.yellow;
        else if (rand == 4)
            return Color.magenta;
        else if (rand == 5)
            return Color.cyan;
        else
            return Color.white;
    }
}
