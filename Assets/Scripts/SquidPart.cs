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

            if (r.HasImpulse)
            {
                c = Color.red;
            }

            if (g.HasImpulse)
            {
                c = Color.green;
            }

            if (b.HasImpulse)
            {
                c = Color.blue;
            }

            if (r.HasImpulse && g.HasImpulse)
            {
                c = Color.yellow;
            }

            if (r.HasImpulse && b.HasImpulse)
            {
                c = Color.magenta;
            }

            if (g.HasImpulse && b.HasImpulse)
            {
                c = Color.cyan;
            }

            if (r.HasImpulse && g.HasImpulse && b.HasImpulse)
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
