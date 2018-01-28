using System.Collections.Generic;
using UnityEngine;

public enum SquidPartType { Top, Left, Right, Total }
public class Squid : MonoBehaviour
{
    [SerializeField]
    List<SquidPart> parts;

    private void Awake()
    {
        foreach (SquidPart part in parts)
            part.Init();
    }
    public Color GetPartColor(SquidPartType type)
    {
        SquidPart part = parts.Find(x => x.GetSquidPartType() == type);

        if (part != null)
            return part.PartColor;

        return Color.clear; //No part will ever intentionally be clear.
    }

    public void SetPartColor(SquidPartType type, Color c)
    {
        //Make the Squid look pretty for the main menu.
        if (GameController.Instance.State == GameState.MainMenu)
            c = Color.white;

        SquidPart part = parts.Find(x => x.GetSquidPartType() == type);
        part.PartColor = c;
    }
}
