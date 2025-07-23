using System;
using UnityEngine;

[Serializable]

public class ColorData
{
    public int ItemID;
    public string Name;
    public int ColorR;
    public int ColorG;
    public int ColorB;

    public Color GetColor(float alpha = 1f)
    {
        return new Color(ColorR / 255f, ColorG / 255f, ColorB / 255f, alpha);
    }
}