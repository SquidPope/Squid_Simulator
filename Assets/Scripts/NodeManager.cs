using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
	List<Node> nodes;

    static NodeManager instance;
    public static NodeManager Instance
    {
		get
		{
			if (instance == null)
			{
				GameObject go = GameObject.FindGameObjectWithTag("NodeManager");
				instance = go.GetComponent<NodeManager>();
			}

			return instance;
		}
    }

	void Start()
	{
		nodes = new List<Node>();
		DontDestroyOnLoad(gameObject);
	}

	public void AddNode(Node node)
	{
		if (!nodes.Contains(node))
			nodes.Add(node);
	}

	public List<Node> GetNodes()
	{
		return nodes;
	}

	public Node GetNodeByID(int id)
	{
		return nodes.Find(x => x.id == id);
	}
}
