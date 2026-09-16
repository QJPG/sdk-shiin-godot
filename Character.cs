using System;
using Godot;

public partial class Character : Godot.RefCounted
{
    private string Name;

    private Color Color;

    public Character(string name, Color color)
    {
        Name = name;
        Color = color;
    }
}
