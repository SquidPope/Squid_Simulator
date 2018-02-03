using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    //LineManager manages connections between nodes and draws corrisponding lines.

    [SerializeField]
    GameObject linePrefab;

    List<LineRenderer> lines;
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

    private void Start()
    {
        nodeConnections = new List<KeyValuePair<int, int>>();

        //Move this to an OnSceneLoad?
        List<Node> nodes = NodeManager.Instance.GetNodes();

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

    public bool DoesConnectionExsist(int idA, int idB)
    {
        Node nodeA = NodeManager.Instance.GetNodeByID(idA);
        Node nodeB = NodeManager.Instance.GetNodeByID(idB);

        if (nodeConnections.Contains(new KeyValuePair<int, int> (nodeA.id, nodeB.id)) || nodeConnections.Contains(new KeyValuePair<int, int>(nodeB.id, nodeA.id)))
            return true;

        return false;
    }

    public void DrawLine(int idA, int idB)
    {
        Node nodeA = NodeManager.Instance.GetNodeByID(idA);
        Node nodeB = NodeManager.Instance.GetNodeByID(idB);

        DrawLine(nodeA, nodeB);
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
