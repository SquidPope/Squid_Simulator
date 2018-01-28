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

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {

        }
        else if (Input.GetMouseButtonUp(1))
        {

        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            quitDialogue.SetActive(true);
        }
    }
}
