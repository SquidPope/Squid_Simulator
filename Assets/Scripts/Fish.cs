using UnityEngine;

public enum FishType { Shark, Tasty, Turtle, Total }
[RequireComponent(typeof(SpriteRenderer))]
public class Fish : MonoBehaviour
{
    [SerializeField]
    Sprite sharkSprite, tastySprite, turtleSprite;

    FishType type;

    public Color correctColor;
    public int correctValue = 0;
    public int wrongValue = 0;

    SpriteRenderer rend;

    public FishType Type
    {
        get { return type; }
        set
        {
            if (rend == null)
            {
                rend = gameObject.GetComponent<SpriteRenderer>();
            }
            correctColor = Color.grey;
            type = value;
            if (type == FishType.Shark)
            {
                rend.sprite = sharkSprite;
                rend.material.color = Color.red;
                correctColor = Color.red;
                correctValue = 1;
                wrongValue = -1;
            }
            else if (type == FishType.Tasty)
            {
                rend.sprite = tastySprite;
                rend.material.color = Color.green;
                correctColor = Color.green;
                correctValue = 1;
            }
            else if (type == FishType.Turtle)
            {
                rend.sprite = turtleSprite;
                rend.material.color = Color.blue;
                correctColor = Color.blue;
                correctValue = 5;
                wrongValue = -2;
            }
        }
    }

    private void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Randomize()
    {
        int rand = Random.Range(0, (int)FishType.Total);
        //If bonus becomes a thing, give it less than a 1/3 chance.
        Type = (FishType)rand;
    }
}
