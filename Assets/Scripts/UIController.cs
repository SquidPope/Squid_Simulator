using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameObject brainStateObject, instructions, helpButton, mainMenuObject, scoreImage, squidStateObject, switchButton;

    [SerializeField]
    Text switchStateButtonLabel, scoreText, finalScoreText;

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

    public void UpdateScore()
    {
        scoreText.text = "Score: " + GameController.Instance.Score.ToString();
    }

    public void UpdateUI()
    {
        GameState state = GameController.Instance.State;
        if (state == GameState.Brain)
        {
            brainStateObject.SetActive(true);
            
            helpButton.SetActive(true);
            mainMenuObject.SetActive(false);
            scoreImage.SetActive(true);
            scoreText.enabled = true;
            squidStateObject.SetActive(false);
            switchButton.SetActive(true);
            PuzzleController.Instance.ShowHideFishies(false);

            switchStateButtonLabel.text = "Solve this Squid!";
        }
        else if (state == GameState.MainMenu)
        {
            brainStateObject.SetActive(false);
            helpButton.SetActive(false);
            mainMenuObject.SetActive(true);
            scoreImage.SetActive(false);
            scoreText.enabled = false;
            squidStateObject.SetActive(false);
            switchButton.SetActive(false);
            PuzzleController.Instance.ShowHideFishies(false);
        }
        else if (state == GameState.Squid)
        {
            brainStateObject.SetActive(false);
            helpButton.SetActive(true);
            mainMenuObject.SetActive(false);
            scoreImage.SetActive(true);
            scoreText.enabled = true;
            squidStateObject.SetActive(true);
            switchButton.SetActive(true);

            switchStateButtonLabel.text = "Brain View";
        }
        else if (state == GameState.GameOver)
        {
            switchButton.SetActive(false);
            finalScoreText.text = "Final Score: " + GameController.Instance.Score;

        }
        
    }
}
