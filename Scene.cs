using Godot;
using System.Threading.Tasks;

[GlobalClass]
public partial class Scene : Node
{
    public static Scene GetCustom()
    {
        SceneTree tree = ((SceneTree)Engine.Singleton.GetMainLoop());
        
        //for (int i = 0; i < tree.Root.GetChildCount(); i++) GD.Print(tree.Root.GetNodeOrNull(i));

        return tree != null ? tree.Root.GetNodeOrNull<Scene>("Scene1") : null;
    }

    public virtual async Task SceneError(Scripting script, params Variant[] args)
    {
        GD.Print("Error Scene: ", args);
    }
}
