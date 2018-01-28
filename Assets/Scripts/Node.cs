using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    List<Node> connectedNodes;

    public int id;
    public bool canTurnOn;

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

    private void Awake()
    {
        rend = gameObject.GetComponent<Renderer>();
        HasNeuron = false;

        Regex regex = new Regex("[0-9]+");
        Match match = regex.Match(gameObject.name);
        Int32.TryParse(match.Value, out id);
    }

    public void SelectNode()
    {
        rend.material.color = Color.yellow;
    }

    public void DeselectNode()
    {
        if (hasNeuron)
            rend.material.color = Color.white;
        else
            rend.material.color = Color.grey;
    }

    public List<Node> GetConnectedNodes()
    {
        return connectedNodes;
    }

    public bool IsConnectedToNode(Node n)
    {
        if (connectedNodes.Contains(n))
            return true;

        return false;
    }
}
