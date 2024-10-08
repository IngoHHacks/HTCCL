using Newtonsoft.Json;

namespace HTCCL.Saves;

internal class MetaFile
{
    private static MetaFile _instance;

    internal static MetaFile Data => _instance ??= Load();

    public List<string> PrefixPriorityOrder { get; set; } = new();
    public bool HidePriorityScreenNextTime { get; set; } = false;

    public bool FirstLaunch { get; set; } = true;

    public int TimesLaunched { get; set; }

    public string PreviousUser { get; set; } = "";

    public void Save()
    {
        string path = Locations.Meta.FullName;
        string json = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(path, json);
        LogDebug($"Saved meta file to {path}.");
    }

    public static MetaFile Load()
    {
        try
        {
            string path = Locations.Meta.FullName;
            if (!File.Exists(path))
            {
                return new MetaFile().IncrementTimesLaunched();
            }

            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<MetaFile>(json,
                    new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace })
                .IncrementTimesLaunched();
        }
        catch (Exception e)
        {
            LogError($"Unable to load meta file: {e}");
            return new MetaFile();
        }
    }

    public MetaFile IncrementTimesLaunched()
    {
        if (this.TimesLaunched != int.MaxValue)
        {
            this.TimesLaunched++;
        }

        return this;
    }
}