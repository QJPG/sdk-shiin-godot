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

public partial class Script : Node
{

    private Dictionary<string, Variant> Definitions;

    [Signal]
    public delegate void ResponseEventHandler();

    public Script()
    {
        Definitions = new Dictionary<string, Variant>();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey key && key.Pressed)
        {
            switch (key.Keycode)
            {
                default:
                    {
                        EmitSignal(SignalName.Response);
                        break;
                    }
            }
        }
    }

    public override async void _Ready()
    {
        await LoadScene("SceneCh0");
        End();
    }

    public async Task LoadScene(string name)
    {
        var method = typeof(Scene).GetMethod(name, BindingFlags.Public | BindingFlags.Static);

        if (method == null)
        {
            GD.PrintErr($"Unknown Scene '{name}'");
            return;
        }

        var invoke = method.Invoke(null, [this]);

        if (invoke != null && invoke is Task task)
        {
            await task;
        }
        else
        {
            GD.PrintErr($"Scene ‘{name}’ was not found or is not an asynchronous Task method!");
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

    public void End()
    {
        GD.Print("Finished!");
    }
}
