using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class LevelData
{
	public string name;
	public int nextNodeID;
	public List<NodeData> nodes;

	public LevelData(string levelName, int nodeID)
    {
        name = levelName;
		nextNodeID = nodeID;
        nodes = new List<NodeData>();
    }

	public LevelData() { }
}

public class NodeData
{
	public int id;
	public Vector2 position; //z is always 0
	public int[] connectedNodeIDs;
	public bool isStart;

	public NodeData() { }
}

public class XMLController : MonoBehaviour
{
	XmlSerializer serializer;
	string path;

	List<LevelData> loadedLevels;

	void Start()
	{
		loadedLevels = new List<LevelData>();
		path = Application.persistentDataPath + "\\SQUIDLevels";
        serializer = new XmlSerializer(typeof (LevelData));
	}

	void Load()
	{
		//Find level files (if any) and load them: list UI so one can be selected
		//if (!File.Exists(path))
            //make one

		//ToDo: Load "base" levels from the Resources folder
	}

	public void Save()
	{
		//Serialize level and save it
		LevelData level = new LevelData();

		List<Node> nodes = LineManager.Instance.GetLevelNodes();

		for (int i = 0; i < nodes.Count; i++)
		{
			Node node = nodes[i];
			NodeData data = new NodeData();
			data.id = node.GetID();
			data.position = node.GetPosition();

			data.connectedNodeIDs = new int[node.GetConnectedNodes().Count];
			for (int j = 0; j < node.GetConnectedNodes().Count; j++)
			{
				data.connectedNodeIDs[j] = node.GetConnectedNodes()[j].GetID();
			}

			level.nodes.Add(data);
		}

		level.name = "SQUID LEVEL TEST";
		level.nextNodeID = level.nodes.Count;
	}
}
