using Godot;
using System;
using System.Threading.Tasks;

[GlobalClass]
public partial class Scene : Node
{
    public virtual async Task SceneError(Scripting scripting, params Variant[] args)
    {
        // Override
    }

    public virtual async Task SceneMenu(Scripting scripting, params Variant[] args)
    {
        // Override
    }

    public virtual async Task SceneEnd(Scripting scripting, params Variant[] args)
    {
        // Override
    }

    public virtual async Task SceneStart(Scripting scripting, params Variant[] args)
    {
        // Override
    }
}
