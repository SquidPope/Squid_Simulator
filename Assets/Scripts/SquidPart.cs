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
            Color c = Color.grey;
           
            //if r && g && b = white
            //if r && b = magenta
            //if r && g = yellow
            //if b && g = cyan
            //if r = red
            //if g = green
            //if b = blue

            if (r.HasNeuron)
            {
                c = Color.red;
            }

            if (g.HasNeuron)
            {
                c = Color.green;
            }

            if (b.HasNeuron)
            {
                c = Color.blue;
            }

            if (r.HasNeuron && g.HasNeuron)
            {
                c = Color.yellow;
            }

            if (r.HasNeuron && b.HasNeuron)
            {
                c = Color.magenta;
            }

            if (g.HasNeuron && b.HasNeuron)
            {
                c = Color.cyan;
            }

            if (r.HasNeuron && g.HasNeuron && b.HasNeuron)
            {
                c = Color.white;
            }

            //Make the squid look pretty for the main menu.
            if (GameController.Instance.State == GameState.MainMenu)
                c = Color.white;

            PartColor = c;
        }
    }

}
