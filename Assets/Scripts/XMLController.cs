using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
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
	[XmlAttribute("id")]
	public int id;
	public Vector2 position; //z is always 0
	public int[] connectedNodeIDs;
	public bool isStart;

	public NodeData() { }
}

public class XMLController : MonoBehaviour
{

	const string fileType = ".squidLevel";

	XmlSerializer serializer;
	string path; //directory path
	List<LevelData> loadedLevels;

	void Start()
	{
		loadedLevels = new List<LevelData>();
		path = Application.persistentDataPath;
        serializer = new XmlSerializer(typeof (LevelData));
	}

	void Load()
	{
		string[] filePaths = Directory.GetFiles(path, "*" + fileType);
		Debug.Log(filePaths.Length + " files found");
		
		LevelData loadedLevel;
		foreach (string file in filePaths)
		{
			//ToDo: Load "base" levels from the Resources folder
			using (FileStream stream = new FileStream(file, FileMode.Open))
			{
				loadedLevel = serializer.Deserialize(stream) as LevelData;
			}

			loadedLevels.Add(loadedLevel);
			Debug.Log("loadedLevel: " + loadedLevel.name);
		}

		LevelEditorController.Instance.LevelName = loadedLevels[0].name;
		LevelEditorController.Instance.BuildLoadedLevel(loadedLevels[0]); //ToDo: move part of this to NodeManager? And set connected nodes list for each node.
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

		Debug.Log("saving " + LevelEditorController.Instance.LevelName);

		if (LevelEditorController.Instance.LevelName == string.Empty)
			LevelEditorController.Instance.LevelName = "blank";

		level.name = LevelEditorController.Instance.LevelName;
		level.nextNodeID = level.nodes.Count;

		//ToDo: check whether we need \ or / cross-platform.
		string filePath = path + "\\" + level.name + fileType; //How will I load without knowing the file names ahead of time?
		Debug.Log("of Exile: " + filePath);

		if (File.Exists(filePath))
		{
			//Save to a file
			using (FileStream stream = new FileStream(filePath, FileMode.Create)) //Do we Create or Append if the level already exsists?
			{
				XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
				serializer.Serialize(writer, level);
			}
		}
		else
		{
			using (FileStream stream = new FileStream(filePath, FileMode.CreateNew))
			{
				XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
				serializer.Serialize(writer, level);
			}
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
