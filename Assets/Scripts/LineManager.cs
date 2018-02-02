using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    //LineManager manages connections between nodes and draws corrisponding lines.

    [SerializeField]
    GameObject linePrefab;

    List<LineRenderer> lines;
    List<Node> nodes;
    List<KeyValuePair<int, int>> nodeConnections;

    static LineManager instance;
    public static LineManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("LineManager");
                instance = go.GetComponent<LineManager>();
            }

            return instance;
        }
    }

    public List<Node> GetLevelNodes()
    {
        return nodes;
    }

    private void Start()
    {
        nodes = new List<Node>();
        nodeConnections = new List<KeyValuePair<int, int>>();

        //Move this to an OnSceneLoad?
        GameObject[] nodeArray = GameObject.FindGameObjectsWithTag("Node");

        if (nodeArray.Length == 0)
            return;

        for (int i = 0; i < nodeArray.Length; i++)
        {
            nodes.Add(nodeArray[i].GetComponent<Node>());
        }

        foreach (Node n in nodes)
        {
            if (n == null)
                Debug.Log("n is null");
            else if (n.GetConnectedNodes() == null)
                Debug.Log("connections for n are null");

            foreach(Node c in n.GetConnectedNodes())
            {
                DrawLine(n, c);
            }
        }
    }

    public bool DoesConnectionExsist(Node nodeA, Node nodeB)
    {
        if (nodeConnections.Contains(new KeyValuePair<int, int> (nodeA.id, nodeB.id)) || nodeConnections.Contains(new KeyValuePair<int, int>(nodeB.id, nodeA.id)))
            return true;

        return false;
    }

    public void DrawLine(Node nodeA, Node nodeB)
    {
        if (DoesConnectionExsist(nodeA, nodeB))
        {
            Debug.LogError("connection already exsists!");
            return;
        }
        else
        {
            LineRenderer line = Instantiate(linePrefab).GetComponent<LineRenderer>();
            line.SetPosition(0, nodeB.transform.position);
            line.SetPosition(1, nodeA.transform.position);
            line.transform.parent = transform;

            KeyValuePair<int, int> keyValue = new KeyValuePair<int, int>(nodeB.id, nodeA.id);
            nodeConnections.Add(keyValue);
        }
    }
}
