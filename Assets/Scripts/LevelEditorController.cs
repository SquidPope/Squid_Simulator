using UnityEngine;
using UnityEngine.UI;

public class LevelEditorController : MonoBehaviour
{
    //ToDo: Add input to name the level (on save?)
    //ToDo: Add input for NeuronLimit, it has to change per level
    //ToDo: Add a way to move placed nodes?
    //ToDo: Add a way to mark a node or nodes as start
    //ToDo: Add input for how much a new node 'snaps' to the grid or, how big the grid is (Slider: 0, 0.5, 0.1, 0.05, 0.01?)
    //ToDo: Draw a grid displaying the 'snap' distances
    //ToDo: Add a way to delete nodes that have been placed.

    public GameObject currentPrefab = null;
    public InputField nameInput;

    Node currentNode = null;
    int nodeID = 0;
    string levelName = "";

    const int uiLayerMask = 1 << 5; //The UI Layer.

    static LevelEditorController instance;
    public static LevelEditorController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("LevelEditorController");
                instance = go.GetComponent<LevelEditorController>();
            }

            return instance;
        }
    }

    public Node CurrentNode
    {
        get { return currentNode; }
        set
        {
            if (currentNode != null)
                currentNode.DeselectNode();

            currentNode = value;

            if (currentNode != null)
                currentNode.SelectNode();
        }
    }

    public string LevelName
    {
        get { return levelName; } //ToDo: move part of this to NodeManager? And set connected nodes list for each node.
        set
        {
            nameInput.text = value; //placeholder text?
            levelName = value;
        }
    }

    public void SetCurrentPrefab(GameObject prefab)
    {
        //ToDo: validation check?
        currentPrefab = prefab;
    }

    public int GetNodeID()
    {
        return nodeID;
    }

    public void BuildLoadedLevel(LevelData level)
    {
        nodeID = level.nextNodeID;

        NodeData current;

        Debug.Log("level building: " + levelName);

        //Create the nodes.
        for (int i = 0; i < level.nodes.Count; i++)
        {
            current = level.nodes[i];
            Vector3 newPos = new Vector3(current.position.x, current.position.y, 0);

            //ToDo: get correct type of prefab for node type
            Node newNode = Instantiate(currentPrefab, newPos, Quaternion.identity).GetComponent<Node>();
            newNode.SetID(current.id);
            NodeManager.Instance.AddNode(newNode);
        }

        //Connect the nodes.
        for (int i = 0; i < level.nodes.Count; i++)
        {
            current = level.nodes[i];
            for (int j = 0; j < current.connectedNodeIDs.Length; j++)
            {
                if (!LineManager.Instance.DoesConnectionExsist(current.id, current.connectedNodeIDs[j]))
                {
                    LineManager.Instance.DrawLine(current.id, current.connectedNodeIDs[j]);
                }
            }
        }
    }

    //Method for InputField.
    public void ChangeLevelName(string name)
    {
        levelName = name;
    }

    Node GetNodeUnderMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        Node n;
        if (hit.collider != null)
        {
            n = hit.collider.GetComponent<Node>();
        }
        else
        {
            n = null;
        }

        return n;
    }

    bool IsUIUnderMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1, uiLayerMask);

        if (hit.collider == null)
            return false;

        Debug.Log("layer: " + hit.collider.gameObject.layer);

        if (hit.collider.gameObject.layer == 5)
            return true;

        return false;
    }

    void Update() //ToDo: Move this to InputManager
    {
        if (GameController.Instance.State == GameState.LevelEditor)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (IsUIUnderMouse())
                    return;

                //ToDo: Make sure it won't collide with another node
                Node node = GetNodeUnderMouse();

                if (currentPrefab != null && node == null)
                {
                    //ToDo: Limit number of nodes placed/how close together they can be placed
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 newPos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0f);

                    Node newNode = Instantiate(currentPrefab, newPos, Quaternion.identity).GetComponent<Node>();
                    newNode.SetID(nodeID);
                    nodeID++; //could use this to limit # of nodes, it's always 1 more than the current nodes.
                }
                else if (currentPrefab == null)
                {
                    Debug.Log("oh noes");
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                if (IsUIUnderMouse())
                    return;

                Node node = GetNodeUnderMouse();

                if (node == null)
                {
                    Debug.Log("current set to null");
                    CurrentNode = null;
                }
                else
                {
                    if (CurrentNode == null)
                    {
                        Debug.Log("setting current");
                        CurrentNode = node;
                    }
                    else
                    {
                        Debug.Log("connecting");
                        if (LineManager.Instance.DoesConnectionExsist(CurrentNode, node))
                            CurrentNode = node; //Is this intuitive?
                        else
                        {
                            currentNode.AddConnectedNode(node);
                            node.AddConnectedNode(currentNode);
                            LineManager.Instance.DrawLine(CurrentNode, node);
                        }
                            
                    }
                }
            }
        }
    }
}
