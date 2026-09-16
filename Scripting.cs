using Godot;
using Godot.Collections;
using System;
using System.Reflection;
using System.Threading.Tasks;

/*
 * 
 * public void PrintArgs(__arglist)
{
    ArgIterator ai = new ArgIterator(__arglist);
    while (ai.GetRemainingCount() > 0)
    {
        var arg = TypedReference.ToObject(ai.GetNextArg());
        Console.WriteLine(arg);
    }
}
*/

[GlobalClass]
public partial class Scripting : Node
{
    public static string CustomSceneNode = "Scene1";

    public static string CustomScriptingNode = "Scripting1";

    public static string CustomGuiNode = "Gui1";

    public static string CustomOptionsNode = "Options1";

    public static Options CustomOptions()
    {
        SceneTree tree = ((SceneTree)Engine.Singleton.GetMainLoop());
        return tree != null ? tree.Root.GetNodeOrNull<Options>(CustomOptionsNode) : null;
    }

    public static Gui CustomGui()
    {
        SceneTree tree = ((SceneTree)Engine.Singleton.GetMainLoop());
        return tree != null ? tree.Root.GetNodeOrNull<Gui>(CustomGuiNode) : null;
    }

    public static Scene CustomScene()
    {
        SceneTree tree = ((SceneTree)Engine.Singleton.GetMainLoop());
        return tree != null ? tree.Root.GetNodeOrNull<Scene>(CustomSceneNode) : null;
    }

    public static Scripting CustomScripting()
    {
        SceneTree tree = ((SceneTree)Engine.Singleton.GetMainLoop());
        return tree != null ? tree.Root.GetNodeOrNull<Scripting>(CustomScriptingNode) : null;
    }

    private Dictionary<string, Variant> Definitions;

    [Signal]
    public delegate void ResponseEventHandler();

    public Scripting()
    {
        Definitions = new Dictionary<string, Variant>();

        Ready += async () =>
        {
            await Call("SceneStart");
            await End();
        };
    }

    public override async void _Input(InputEvent @event)
    {
        if (@event is InputEventKey key && key.Pressed)
        {
            switch (key.Keycode)
            {
                case Key.Escape:
                    {
                        await Call("SceneMenu");
                        break;
                    }

                default:
                    {
                        EmitSignal(SignalName.Response);
                        break;
                    }
            }
        }
    }

    public async Task Call(string name, params Variant[] args)
    {
        Scene scene = CustomScene();
        MethodInfo method = null;

        if (scene != null)
        {
            method = scene.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (method == null)
            {
                if (name != "SceneError") await Call("SceneError", $"Unknown custom scene method '{name}'");
                return;
            }

            var invoke = method.Invoke(scene, [this, args]);

            if (invoke != null && invoke is Task task)
            {
                await task;
            }
            else
            {
                GD.PrintErr($"Scene method ‘{name}’ was not found or is not an asynchronous Task method!");
            }
        }
        else
        {
            GD.PrintErr($"No custom scene found: Add a autoload script that inherits from the Scene class and is named “{CustomSceneNode}”.");
        }
    }

    public void Define(string name, Variant value)
    {
        Definitions[name] = value;
    }

    public async Task Exec(string argv, params Variant[] args)
    {
        switch (argv)
        {
            case "scene":
                {
                    if (args.Length > 0)
                    {
                        switch (args[0].AsString())
                        {
                            case "show":
                                {
                                    var x = 0.0f;
                                    args[2].As<Dissolve>().Add(a => x = a);
                                    await args[2].As<Dissolve>().Play();
                                    GD.Print(x);
                                    break;
                                }

                            default:
                                {
                                    GD.PrintErr("Unknown scene argument: ", args[0]);
                                    break;
                                }
                        }
                    }
                    else
                    {
                        GD.PrintErr("Expected 1 or more arguments to scene");
                    }
                    break;
                }

            default:
                {
                    break;
                }
        }

        await ToSignal(this, SignalName.Response);
    }

    public async Task End()
    {
        await Call("SceneEnd"); //  Bye! Bye!
    }
}
