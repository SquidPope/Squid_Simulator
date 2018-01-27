using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    List<Node> connectedNodes;

    [SerializeField]
    GameObject linePrefab;

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

        //ToDo: remove duplicate lines (there's a lot of them)
        foreach (Node n in connectedNodes)
        {
            if (n == null)
                continue;

            GameObject go = Instantiate(linePrefab);
            LineRenderer line = go.GetComponent<LineRenderer>();
            line.SetPosition(0, gameObject.transform.position);
            line.SetPosition(1, n.transform.position);
        }
    }

    public bool IsConnectedToNode(Node n)
    {
        if (connectedNodes.Contains(n))
            return true;

        return false;
    }
}
