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
            //Physics2D.Raycast(gameObject.transform.position, n.gameObject.transform.position);
            GameObject go = Instantiate(linePrefab);
            LineRenderer line = go.GetComponent<LineRenderer>();
            line.SetPosition(0, gameObject.transform.position);
            line.SetPosition(1, n.transform.position);
        }
    }

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
    }
}
