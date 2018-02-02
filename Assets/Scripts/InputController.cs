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
        DontDestroyOnLoad(gameObject);
    }

    public void PlayButton()
    {
        GameController.Instance.State = GameState.Squid;
        SceneManager.LoadScene("level");
        PuzzleController.Instance.StartPuzzle();
    }

    public void LevelEditorButton()
    {
        GameController.Instance.State = GameState.LevelEditor;
        SceneManager.LoadScene("levelEditorTest");
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

    private void Update()
    {
        Node clickedNode = GetNodeUnderMouse(); //The node we clicked this frame.

        if (clickedNode != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                //if clickedNode is start
                //select clickedNode, clickedNode.HasImpulse = true

                //if selectedNode == null

                //if selectedNode is null
                //??
                //if it is connected to selectedNode
                //are they both on?
                //no - swap yes - select clickedNode and deselect selectedNode

                if (clickedNode.isStart)
                {
                    if (selectedNode != null)
                        selectedNode.DeselectNode();

                    clickedNode.HasImpulse = true;
                    clickedNode.SelectNode();
                    selectedNode = clickedNode;
                }
                else if (selectedNode != null && clickedNode.GetConnectedNodes().Contains(selectedNode))
                {
                    if (!clickedNode.HasImpulse)
                    {
                        selectedNode.HasImpulse = false;
                        clickedNode.HasImpulse = true;
                    }

                    selectedNode.DeselectNode();
                    clickedNode.SelectNode();
                    selectedNode = clickedNode;
                }
                else if (clickedNode.HasImpulse)
                {
                    selectedNode.DeselectNode();
                    clickedNode.SelectNode();
                    selectedNode = clickedNode;
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                clickedNode.HasImpulse = false;

                if (clickedNode == selectedNode)
                {
                    clickedNode.DeselectNode();
                    selectedNode = null;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            quitDialogue.SetActive(true);
        }
    }
}
