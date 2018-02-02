using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    List<Node> connectedNodes;

    public int id = 0;
    public bool isStart;

    private bool hasImpulse;
    Renderer rend;

    public bool HasImpulse
    {
        get { return hasImpulse; }
        set
        {
            hasImpulse = value;
            if (hasImpulse)
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
        HasImpulse = false;

        Init();
    }

    public void Init()
    {
        //ToDo: make sure no number in name gets id 0
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
        if (hasImpulse)
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

    public bool AddConnectedNode(Node node)
    {
        if (connectedNodes.Contains(node))
            return false;

        connectedNodes.Add(node);
        return true;
    }
}
