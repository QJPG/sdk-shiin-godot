using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Options : Node
{
    private Dictionary<string, Variant> Defaults;

    public string savePath = "user://options.cfg";

    public string versionName = ProjectSettings.GetSetting("application/config/version", "").AsString();

    public string authorName = ProjectSettings.GetSetting("application/config/author", "").AsString();

    public string gameName = ProjectSettings.GetSetting("application/config/name", "").AsString();

    public string gameDescription = ProjectSettings.GetSetting("application/config/description", "").AsString();

    public Options()
    {
        Defaults = new Dictionary<string, Variant>();
        Load();
    }

    public void SetDefault(string key, Variant value)
    {
        Defaults[key] = value;
    }

    public Variant GetDefault(string key)
    {
        return Defaults[key];
    }

    public void Save()
    {
        var config = new ConfigFile();
        foreach (var kvp in Defaults)
        {
            config.SetValue("options", kvp.Key, kvp.Value);
        }
        config.Save(savePath);
    }

    public void Load()
    {
        var config = new ConfigFile();
        var err = config.Load(savePath);
        if (err == Error.Ok)
        {
            foreach (var kvp in Defaults)
            {
                var value = config.GetValue("options", kvp.Key, kvp.Value);
                Defaults[kvp.Key] = value;
            }
        }
    }
}
