using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Prompt : MonoBehaviour {

    [SerializeField]
    Text text;

    string match = "Match!";
    string help = "Guide!";

    float lifespan = 3f;
    float timer;
    float fadeDelay = 1f;

    bool isShowing = false;

    Renderer rend;

    private void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    public void Show()
    {
        isShowing = true;
        if (PuzzleController.Instance.GetCurrentPuzzleType() == PuzzleType.Help)
        {
            text.text = help;
        }
        else if (PuzzleController.Instance.GetCurrentPuzzleType() == PuzzleType.Match)
        {
            text.text = match;
        }
    }

    public IEnumerator FadeText (float f, Text t)
    {
        t.color = new Color(t.color.r, t.color.g, t.color.b, 1f);
        while (t.color.a > 0f)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a - (Time.deltaTime / f));
            yield return null;
        }
        isShowing = false;
    }

    private void Update()
    {
        if (isShowing)
        {
            timer += Time.deltaTime;
            if (timer >= lifespan)
            {
                //fade out
                StartCoroutine(FadeText(fadeDelay, text));
                return;
            }
        } 
    }
}
