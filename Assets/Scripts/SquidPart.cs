using UnityEngine;

public class SquidPart : MonoBehaviour {
    //A part of the display squid that can be colored/tinted separatly.

    [SerializeField]
    Node r, g, b;

    [SerializeField]
    SquidPartType type;

    Renderer rend;
    Color partColor;

    public Color PartColor
    {
        get { return partColor; }
        set
        {
            partColor = value;
            rend.material.color = partColor;
        }
    }

    private void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        rend.material.color = Color.black;
        partColor = rend.material.color;
    }

    public SquidPartType GetSquidPartType()
    {
        return type;
    }

    //ToDo: try to do this on a part changing, not every frame.
    private void Update()
    {
        Color c = Color.black;

        if (r.HasNeuron)
            c.r = 100;

        if (g.HasNeuron)
            c.g = 100;

        if (b.HasNeuron)
            c.b = 100;

        PartColor = c;
    }
}
