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
            //ToDo: start some nodes on, or right click to turn on.
            if (Input.GetMouseButtonDown(0))
            {
                clickedNode = GetNodeUnderMouse();
                if (clickedNode != null && !clickedNode.HasNeuron)
                {
                    clickedNode.HasNeuron = true;
                    clickedNode = null;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (clickedNode != null)
                {
                    Node current = GetNodeUnderMouse();
                    //check if the nodes are connected
                    if (current.IsConnectedToNode(clickedNode))
                    {
                        clickedNode.HasNeuron = false;
                        current.HasNeuron = true;
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (quitDialogue.active == true)
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

    public void YesQuit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void NoQuit()
    {
        quitDialogue.SetActive(false);
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
