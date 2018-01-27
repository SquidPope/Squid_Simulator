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

    //ToDo: use click and drag
    /*
     * OnMouseDown: remove neuron if it exsists (effect on/near mouse to make obvious
     * OnMouseUp: down was on a connected node and this node is off, turn it on (if this node is on, revert)
     */

    /*
    private void OnMouseUp()
    {
        if (HasNeuron)
        {
            //error
        }
        else
        {
            bool switched = false;
            HasNeuron = true;
            //light up
            foreach (Node n in connectedNodes)
            {
                if (n == null)
                    continue;

                //ToDo: Let the player choose where they want to move the neuron.
                if (n.HasNeuron)
                {
                    n.HasNeuron = false;
                    switched = true;
                    break; //Only take one neuron
                }
            }

            if (!switched)
            {
                if (GameController.Instance.totalNeurons + 1 > GameController.Instance.NeuronLimit)
                {
                    HasNeuron = false;
                }
                else
                {
                    GameController.Instance.totalNeurons++;
                }
            }
        }
    }*/
}
