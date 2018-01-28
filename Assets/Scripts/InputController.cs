using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    [SerializeField]
    GameObject quitDialogue;
    
    Node selectedNode = null;

    private void Start()
    {
        quitDialogue.SetActive(false);
    }

    void Update()
    {
		if (GameController.Instance.State == GameState.Brain)
        {

            //Click the start node to turn it on and select it.
            //Click other nodes that are next to the currently selected node to move the electron.
            //OR click other nodes that are on to select them instead.
            //Switching electrons gets priority over creating new ones at the start node.
            #region Mouse State
            if (Input.GetMouseButtonDown(0))
            {
                if (selectedNode != null)
                {
                    Node current = GetNodeUnderMouse();
                    if (current == null)
                        return;

                    if (current.IsConnectedToNode(selectedNode))
                    {
                        //if the new node already has a neuron, just select it instead (don't remove neuron) because we aren't "switching".
                        if (!current.HasNeuron)
                        {
                            selectedNode.HasNeuron = false;
                            current.HasNeuron = true;
                        }

                        selectedNode.DeselectNode();
                        selectedNode = current;
                        current.SelectNode();
                    }
                    else if (current.HasNeuron || current.canTurnOn)
                    {
                        selectedNode.DeselectNode();
                        selectedNode = current;
                        selectedNode.HasNeuron = true;
                        selectedNode.SelectNode();
                    }
                }
                else
                {
                    Node current = GetNodeUnderMouse();
                    if (current != null && (current.HasNeuron || current.canTurnOn))
                    {
                        selectedNode = current;
                        selectedNode.HasNeuron = true;
                        selectedNode.SelectNode();
                    }
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                Node current = GetNodeUnderMouse();
                if (current != null)
                {
                    current.HasNeuron = false;

                    if (selectedNode == current)
                        selectedNode = null;
                } 
            }
            #endregion
        }
        else
        {
            //Deselect the node because having it already selected could throw players off.
            if (selectedNode != null)
            {
                selectedNode.DeselectNode();
                selectedNode = null;
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (quitDialogue.activeSelf == true)
            {
                Application.Quit();
            }
            else
            {
                quitDialogue.SetActive(true);
            }
        }
    }

    public void PlayButton()
    {
        GameController.Instance.State = GameState.Squid;
        PuzzleController.Instance.StartPuzzle();
    }

    public void QuitButton()
    {
        quitDialogue.SetActive(true);
    }

    public void YesQuit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void NoQuit()
    {
        quitDialogue.SetActive(false);
    }

    public void Reset()
    {
        //Just reload the whole dumb thing.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SwitchState()
    {
        //Prevent the player from switching before the next puzzle has started.
        if (PuzzleController.Instance.GetIsSolved())
            return;

        if (GameController.Instance.State == GameState.Brain)
        {
            GameController.Instance.State = GameState.Squid;
        }
        else if (GameController.Instance.State == GameState.Squid)
        {
            GameController.Instance.State = GameState.Brain;
        }
    }

    Node GetNodeUnderMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        Node n;
        if (hit != null && hit.collider != null)
        {
            n = hit.collider.GetComponent<Node>();
        } 
        else
        {
            n = null;
        }

        return n;
    }
}
