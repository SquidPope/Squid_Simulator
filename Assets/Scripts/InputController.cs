using UnityEngine;

public class InputController : MonoBehaviour
{
    Node clickedNode = null;

	void Update()
    {
		if (GameController.Instance.state == GameState.Brain)
        {
            //ToDo: start some nodes on, or right click to turn on.
            if (Input.GetMouseButtonDown(0))
            {
                clickedNode = GetNodeUnderMouse();
                if (!clickedNode.HasNeuron)
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
