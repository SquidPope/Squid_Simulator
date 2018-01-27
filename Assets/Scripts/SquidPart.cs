using UnityEngine;

public class SquidPart : MonoBehaviour {
    //A part of the display squid that can be colored/tinted separatly.

    [SerializeField]
    Node r, g, b;

    Renderer rend;

    private void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        rend.material.color = Color.black;
    }

    private void Update()
    {
        Color c = Color.black;

        if (r.HasNeuron)
            c.r = 100;

        if (g.HasNeuron)
            c.g = 100;

        if (b.HasNeuron)
            c.b = 100;

        rend.material.color = c;
    }
}
