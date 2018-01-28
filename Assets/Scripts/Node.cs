using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    List<Node> connectedNodes;

    [SerializeField]
    GameObject linePrefab;

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

    public void SelectNode()
    {
        rend.material.color = Color.yellow;
    }

    public List<Node> GetConnectedNodes()
    {
        return connectedNodes;
    }

    private void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        HasNeuron = false;

        Regex regex = new Regex("[0-9]+");
        Match match = regex.Match(gameObject.name);
        Int32.TryParse(match.Value, out id);
        
        //ToDo: remove duplicate lines (there's a lot of them)
        foreach (Node n in connectedNodes)
        {
            if (n == null)
                continue;

            GameObject go = Instantiate(linePrefab);
            LineRenderer line = go.GetComponent<LineRenderer>();
            line.SetPosition(0, gameObject.transform.position);
            line.SetPosition(1, n.transform.position);
            line.transform.parent = gameObject.transform;
        }
    }

    public bool IsConnectedToNode(Node n)
    {
        if (connectedNodes.Contains(n))
            return true;

        return false;
    }
}
