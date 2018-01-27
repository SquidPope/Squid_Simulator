using System.Collections.Generic;
using UnityEngine;

public enum SquidPartType { Top, Left, Right, Total }
public class Squid : MonoBehaviour
{
    [SerializeField]
    List<SquidPart> parts;

    public Color GetPartColor(SquidPartType type)
    {
        SquidPart part = parts.Find(x => x.GetSquidPartType() == type);

        if (part != null)
            return part.PartColor;

        return Color.clear; //No part will ever intentionally be clear.
    }
}
