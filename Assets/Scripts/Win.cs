using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField]
    Material particleMat;

    ParticleSystem ps;
    Renderer particleRenderer;
    float delay = 1f;
    float delayTimer;

    private void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();

        if (ps == null)
            Debug.Log("Error! No particle system for the win");

        particleRenderer = ps.GetComponent<Renderer>();
    }

    private void Update()
    {
        delayTimer += Time.deltaTime;
        if (delayTimer >= delay)
        {
            delayTimer = 0f;
            particleMat.color = RandomColor();
            ps.Emit(20);
        }
    }

    private Color RandomColor()
    {
        int rand = Random.Range(0, 7);
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
