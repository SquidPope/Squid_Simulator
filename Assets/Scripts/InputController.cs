using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    GameObject quitDialogue;
    
    Node clickedNode = null;

    private void Start()
    {
        quitDialogue.SetActive(false);
    }

    void Update()
    {
		if (GameController.Instance.State == GameState.Brain)
        {
            //ToDo: start some nodes on, or just left click to turn on?

            //ToDo: clickedNode should be a different color so the player knows what is selected.
            #region Mouse State
            if (Input.GetMouseButtonDown(0))
            {
                if (clickedNode != null)
                {
                    Node current = GetNodeUnderMouse();
                    if (current == null)
                        return;

                    //check if the nodes are connected
                    if (current.IsConnectedToNode(clickedNode))
                    {
                        clickedNode.HasNeuron = false;
                        current.HasNeuron = true;

                        clickedNode = null;
                    }
                }
                else
                {
                    Node current = GetNodeUnderMouse();
                    if (current != null && (current.HasNeuron || current.canTurnOn))
                    {
                        clickedNode = current;
                        clickedNode.HasNeuron = true;
                        clickedNode.SelectNode();
                    }
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                Node current = GetNodeUnderMouse();
                if (current != null)
                {
                    current.HasNeuron = false;

                    if (clickedNode == current)
                        clickedNode = null;
                } 
            }
            #endregion

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
	}

    public void PlayButton()
    {
        GameController.Instance.State = GameState.Squid;
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

    public void SwitchState()
    {
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
