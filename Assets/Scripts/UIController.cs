using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameObject brainStateObject, instructions, helpButton, mainMenuObject, squidStateObject, switchButton;

    [SerializeField]
    Text switchStateButtonLabel;

    private static UIController instance;
    public static UIController Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("UIController");
                instance = go.GetComponent<UIController>();
            }
            return instance;
        }
    }

    public void HelpButton()
    {
        instructions.SetActive(!instructions.activeSelf);
    }

    public void UpdateUI()
    {
        GameState state = GameController.Instance.State;
        if (state == GameState.Brain)
        {
            brainStateObject.SetActive(true);
            helpButton.SetActive(true);
            mainMenuObject.SetActive(false);
            squidStateObject.SetActive(false);
            switchButton.SetActive(true);
            switchStateButtonLabel.text = "Squid View";
        }
        else if (state == GameState.MainMenu)
        {
            brainStateObject.SetActive(false);
            helpButton.SetActive(false);
            mainMenuObject.SetActive(true);
            squidStateObject.SetActive(false);
            switchButton.SetActive(false);
        }
        else if (state == GameState.Squid)
        {
            brainStateObject.SetActive(false);
            helpButton.SetActive(true);
            mainMenuObject.SetActive(false);
            squidStateObject.SetActive(true);
            switchButton.SetActive(true);
            switchStateButtonLabel.text = "Brain View";
        }
        
    }
}
