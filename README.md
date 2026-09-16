# SDK-Shiin
Shiin is a project for developing games entirely through scripting, based on the Ren'py engine.
This project was built specifically for the Godot Engine using C#. 
> This isn't a plugin; just copy this SDK into your project directory and use it however you like.

## godot
Some autoloads are required for initialization. Keep these autoloads in the order listed below:
- Options1 -> Options.cs
- Gui1 -> Gui.cs
- Scripting1 -> Scripting.cs
- Scene1 -> Scene.cs

> All of these autoloads must inherit from the classes already defined by the SDK.

The names are default values, but they can be changed by modifying the following fields:
```cs
    public partial class Scripting : Node
    {
        public static string CustomSceneNode = “Scene1”;

        public static string CustomScriptingNode = “Scripting1”;

        public static string CustomGuiNode = “Gui1”;

        public static string CustomOptionsNode = “Options1”;
        ...
```

### Scenes
In your ‘Scene1.cs’ script, almost all methods are treated as scenes.
They can be called directly within the class or using the ```Scripting.Call(‘SceneMethodName’,  ..args)``` method.

These methods must be **asynchronous** and return the **Task** type.

Some examples of standard scene methods:
```cs
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

    // In Script1.cs
    //
    public async Task SceneMyCustomScene(Scripting scripting, params Variant[] args)
    {
        // Write your storyteller!
    }
```