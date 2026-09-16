using Godot;
using System;
using System.Threading.Tasks;

public static class Scene
{
    public static async Task SceneCh0(Script script)
    {
        script.Define("e", new Character("Eileen", new(1, 1, 1, 1)));

        await script.Exec("e", "Oh.. Maybe.. Yeah?");
        await script.Exec("scene", "show", "bg1", new Dissolve(0, 1, 0.15f));

        script.LoadScene("SceneCh1");
    }

    public static async Task SceneCh1(Script script)
    {
        GD.Print("Hello from scene 1");
    }
}
