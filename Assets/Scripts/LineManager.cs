using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField]
    GameObject linePrefab;

    List<LineRenderer> lines;
    List<Node> nodes;
    List<KeyValuePair<int, int>> nodeConnections;

    private void Start()
    {
        nodes = new List<Node>();
        nodeConnections = new List<KeyValuePair<int, int>>();
        GameObject[] nodeArray = GameObject.FindGameObjectsWithTag("Node");

        for (int i = 0; i < nodeArray.Length; i++)
        {
            nodes.Add(nodeArray[i].GetComponent<Node>());
        }

        Debug.Log("there are " + nodes.Count + (" nodes."));

        foreach (Node n in nodes)
        {
            if (n == null)
                Debug.Log("n is null");
            else if (n.GetConnectedNodes() == null)
                Debug.Log("connections for n are null");
            foreach(Node c in n.GetConnectedNodes())
            {
                if (nodeConnections.Contains(new KeyValuePair<int, int> (n.id, c.id)) || nodeConnections.Contains(new KeyValuePair<int, int>(c.id, n.id)))
                    continue;

                LineRenderer line = Instantiate(linePrefab).GetComponent<LineRenderer>();
                line.SetPosition(0, c.transform.position);
                line.SetPosition(1, n.transform.position);

                KeyValuePair<int, int> keyValue = new KeyValuePair<int, int>(c.id, n.id);
                nodeConnections.Add(keyValue);
            }
        }
    }
}
