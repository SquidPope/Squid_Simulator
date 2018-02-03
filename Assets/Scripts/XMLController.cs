using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class LevelData
{
	[XmlAttribute("name")]
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

	LevelData loadedLevel;//Save/Load one level for now.

	void Start()
	{
		//loadedLevels = new List<LevelData>();
		path = Application.persistentDataPath; // + "\\SQUIDLevels";
        serializer = new XmlSerializer(typeof (LevelData));
	}

	void Load()
	{
		string levelPath = path + "\\" + "SQUID LEVEL TEST.squidLevel";
		//Find level files (if any) and load them: list UI so one can be selected
		if (File.Exists(levelPath))
        {
			//ToDo: Load "base" levels from the Resources folder
			using (FileStream stream = new FileStream(levelPath, FileMode.Open))
			{
				loadedLevel = serializer.Deserialize(stream) as LevelData;
			}

			Debug.Log("loadedLevel: " + loadedLevel.name);
			LevelEditorController.Instance.BuildLoadedLevel(loadedLevel);
		}
	}

	public void Save()
	{
		//Serialize level and save it
		LevelData level = new LevelData("test", LevelEditorController.Instance.GetNodeID());

		GameObject[] nodeArray = GameObject.FindGameObjectsWithTag("Node");

		for (int i = 0; i < nodeArray.Length; i++)
		{
			Node node = nodeArray[i].GetComponent<Node>();
			NodeData data = new NodeData();
			data.id = node.GetID();
			data.position = node.GetPosition();

			data.connectedNodeIDs = new int[node.GetConnectedNodes().Count];
			for (int j = 0; j < node.GetConnectedNodes().Count; j++)
			{
				if (node.GetConnectedNodes()[j] != null)
					data.connectedNodeIDs[j] = node.GetConnectedNodes()[j].GetID();
			}

			level.nodes.Add(data);
		}

		level.name = "SQUID LEVEL TEST";
		level.nextNodeID = level.nodes.Count;
		Debug.Log("nodes count " + level.nextNodeID);

		path += "\\" + level.name + ".squidLevel"; //How will I load without knowing the file names ahead of time?
		Debug.Log("of Exile: " + path);
		//Save to a file
		using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
            serializer.Serialize(writer, level);
        }
	}

//ToDo: remove when UI works
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.S))
			Save();
		else if (Input.GetKeyUp(KeyCode.L))
			Load();
	}
}
