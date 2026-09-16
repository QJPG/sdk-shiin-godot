using Godot;
using System;
using System.Threading.Tasks;

public partial class Dissolve : RefCounted
{
    private Tween Tweener;

    private float From;

    private float To;

    private float Time;

    public Dissolve(float from, float to, float time)
    {
        From = from;
        To = to;
        Time = time;
        Tweener = ((SceneTree)Engine.Singleton.GetMainLoop()).CreateTween();
        Tweener.Pause();
    }

    public MethodTweener Add(Action<float> setter)
    {
        return Tweener.Parallel().TweenMethod(Callable.From<float>(setter), From, To, Time);
    }

    public async Task Play()
    {
        Tweener.Play();
        await ToSignal(Tweener, Tween.SignalName.Finished);
    }
}
