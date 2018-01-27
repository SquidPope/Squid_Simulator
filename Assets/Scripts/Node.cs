using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    List<Node> connectedNodes;

    private bool hasNeuron;
    Renderer rend;

    public bool HasNeuron
    {
        get { return hasNeuron; }
        set
        {
            hasNeuron = value;
            if (hasNeuron)
            {
                rend.material.color = Color.white;
            }
            else
            {
                rend.material.color = Color.gray;
            }
        }
    }

    private void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        HasNeuron = false;
    }

    private void OnMouseUp()
    {
        if (HasNeuron)
        {
            //error
        }
        else
        {
            HasNeuron = true;
            //light up
            foreach (Node n in connectedNodes)
            {
                if (n.HasNeuron)
                {
                    n.HasNeuron = false;
                    break; //Only take one neuron
                }
            }
        }
    }
}
