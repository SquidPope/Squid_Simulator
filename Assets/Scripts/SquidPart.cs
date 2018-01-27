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

    public void Init()
    {
        rend = gameObject.GetComponent<Renderer>();
        rend.material.color = Color.black;
        partColor = rend.material.color;
    }

    public SquidPartType GetSquidPartType()
    {
        return type;
    }

    //ToDo: try to do this on a part changing, not every frame. (should only happen to player squid)
    private void Update()
    {
        if (r != null && g != null && b != null)
        {
            Color c = Color.black;

            if (r.HasNeuron && g.HasNeuron && b.HasNeuron)
            {
                c = Color.white;
            }
            else if (r.HasNeuron)
            {
                if (g.HasNeuron)
                    c = Color.yellow;
                else if (b.HasNeuron)
                    c = Color.magenta;
                else
                    c = Color.red;
            } 
            else if (g.HasNeuron)
            {
                if (b.HasNeuron)
                    c = Color.cyan;
                else
                    c = Color.green;

                return;
            }
            else if (b.HasNeuron)
                c = Color.blue;

            PartColor = c;
        }
    }

}
