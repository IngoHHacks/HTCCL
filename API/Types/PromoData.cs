namespace HTCCL.API.Types;

public class PromoData
{
    public const int BASE_PROMO_OFFSET = 1000000;
    public const int CODE_PROMO_OFFSET = 2000000;

    public enum CareerModeProbability
    {
        Disabled,
        Absolute,
        GroupedRelative,
        Relative
    }
    
    public List<PromoLine> PromoLines { get; set; } = new();

    public string Title { get; set; } = "Title";
    public string Description { get; set; } = "Description";
    
    public string Category { get; set; } = "Custom";

    internal int _id = -1;

    public int[] Characters { get; set; } = new int[3];
    public bool UseCharacterNames { get; set; } = false;
    
    public CareerModeProbability CareerModeProbabilitySetting { get; set; } = CareerModeProbability.Disabled;
    
    public string GroupName { get; set; } = "";
    
    public float CareerModeProbabilityValue { get; set; } = 0f;
    public bool Force { get; set; } = false;
    
    public Dictionary<string, int> NameToID { get; set; } = new Dictionary<string, int>();
    public HashSet<int> SurpriseEntrants { get; set; } = new HashSet<int>();
    public HashSet<int> SurpirseExtras { get; set; } = new HashSet<int>();
    public HashSet<string> SurpriseEntrantsNames { get; set; } = new HashSet<string>();
    public string NextPromo { get; set; } = "";

    public int NumLines => this.PromoLines.Count;

    public static PromoData FromString(List<string> lines)
    {
        try
        {
            PromoData promoData = new PromoData();
            int c;
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                if (line.Trim().Length == 0)
                {
                    continue;
                }

                if (line.ToLower().StartsWith("title:"))
                {
                    promoData.Title = line.Substring(6).Trim();
                    continue;
                }

                if (line.ToLower().StartsWith("description:"))
                {
                    promoData.Description = line.Substring(12).Trim();
                    continue;
                }

                if (line.ToLower().StartsWith("characters:"))
                {
                    if (line.Substring(11).Trim().StartsWith(":"))
                    {
                        int num = int.Parse(line.Substring(11).Trim().Substring(1));
                        promoData.Characters = GenerateArrayForNum(num);
                    }
                    else
                    {
                        IEnumerable<int> chars = line.Substring(11).Trim().Split(',').Select(x => int.Parse(x.Trim()));
                        IEnumerable<int> enumerable = chars.ToList();
                        if (enumerable.First() != 0)
                        {
                            enumerable = enumerable.Prepend(0);
                        }

                        promoData.Characters = enumerable.ToArray();
                    }

                    continue;
                }

                if (line.ToLower().Replace("-", "_").StartsWith("use_names:"))
                {
                    bool.TryParse(line.Substring(10).Trim(), out bool useNames);
                    promoData.UseCharacterNames = useNames;
                    continue;
                }

                if (line.ToLower().Replace("-", "_").StartsWith("surprise_entrants:"))
                {
                    if (!promoData.UseCharacterNames)
                    {
                        promoData.SurpriseEntrants = new HashSet<int>(line.Substring(19).Trim().Split(',').Select(x => int.Parse(x.Trim())).ToList());
                    }
                    else
                    {
                        promoData.SurpriseEntrantsNames = new HashSet<string>(line.Substring(19).Trim().Split(',').Select(x => x.Trim()).ToList());
                    }
                    continue;
                }

                if(line.ToLower().Replace("-", "_").StartsWith("next_promo:"))
                {
                    promoData.NextPromo = line.Substring(11).Trim();
                    continue;
                }
                
                if (line.ToLower().Replace("-", "_").StartsWith("career_probability:"))
                {
                    string meta = line.Substring(19).Trim();
                    string arg = "";
                    if (meta.Contains(" "))
                    {
                        arg = meta.Substring(meta.IndexOf(' ')).Trim().Replace(",", "");
                        meta = meta.Substring(0, meta.IndexOf(' '));
                    }

                    if (meta.EndsWith("!"))
                    {
                        promoData.Force = true;
                        meta = meta.Substring(0, meta.Length - 1);
                    }
                    if (Enum.TryParse(meta, true, out CareerModeProbability careerModeProbability))
                    {
                        promoData.CareerModeProbabilitySetting = careerModeProbability;
                        if (careerModeProbability is CareerModeProbability.Absolute or CareerModeProbability.Relative)
                        {
                            promoData.CareerModeProbabilityValue = float.Parse(arg);
                        }
                        else if (careerModeProbability is CareerModeProbability.GroupedRelative)
                        {
                            string arg2 = arg.Substring(arg.IndexOf(' ')).Trim();
                            float weight = float.Parse(arg.Substring(0, arg.IndexOf(' ')));
                            promoData.CareerModeProbabilityValue = weight;
                            promoData.GroupName = arg2;
                        }
                    }
                }
                
                if (line.ToLower().StartsWith("category:"))
                {
                    promoData.Category = line.Substring(9).Trim();
                    continue;
                }
                
                c = 0;
                PromoLine promoLine = new PromoLine();
                bool stringMode = false;
                string currentString = "";
                bool escape = false;
                int last = -1;
                for (int j = 0; j < line.Length; j++)
                {
                    char ch = line[j];
                    if (escape)
                    {
                        currentString += ch;
                        escape = false;
                        continue;
                    }

                    if (ch == '\\')
                    {
                        escape = true;
                        continue;
                    }

                    if (ch == '"')
                    {
                        stringMode = !stringMode;
                        continue;
                    }

                    if (ch == ',' && !stringMode)
                    {
                        switch (c)
                        {
                            case 0:
                                promoLine.Line1 = currentString.Trim();
                                break;
                            case 1:
                                promoLine.Line2 = currentString.Trim();
                                break;
                        }

                        c++;
                        currentString = "";
                        if (c == 2)
                        {
                            last = j;
                            break;
                        }

                        continue;
                    }

                    currentString += ch;
                }

                if (last == -1)
                {
                    continue;
                }

                string[] meta2 = line.Substring(last + 1).Split(',');
                if(promoData.UseCharacterNames)
                {
                    promoLine.FromName = meta2.Length > 0 ? meta2[0].Trim() : "";
                    promoLine.ToName = meta2.Length > 1 ? meta2[1].Trim() : "";
                }
                else
                {
                    promoLine.From = meta2.Length > 0 ? int.Parse(meta2[0].Trim()) : 1;
                    promoLine.To = meta2.Length > 1 ? int.Parse(meta2[1].Trim()) : 2;
                }

                promoLine.TauntAnim = meta2.Length > 2 ? Indices.ParseTauntAnim(meta2[2].Trim()) : 0;
                promoLine.Demeanor = meta2.Length > 3 ? float.Parse(meta2[3].Trim()) : 0;
                promoLine.Features = meta2.Length > 4 ? SetUpFeatures(meta2[4].Trim()) : null;
                promoData.PromoLines.Add(promoLine);
            }

            return promoData;
        }
        catch (Exception e)
        {
            LogError($"Error parsing promo data: {e}");
            return null;
        }
    }
    public bool IsCharacterSurprise(UnmappedPlayer character)
    {
        if (UseCharacterNames && SurpriseEntrantsNames.Contains(character.EMDMDLNJFKP.name))
        {
            return true;
        }
        else if (SurpriseEntrants.Contains(character.PLFGKLGCOMD))
        {
            return true;
        }
        return false;
    }
    //format: command:arg:arg;command:arg
    public static List<AdvFeatures> SetUpFeatures(string commands)
    {
        List<AdvFeatures> advFeatures = new();
        string[] features = commands.Split(';');
        foreach (string feature in features)
        {
            string[] command = feature.Split(':');
            AdvFeatures advFeature = new AdvFeatures();
            if (!advFeature.SetCommand(command[0]))
            {
                throw new Exception($"Invalid command: {command[0]}");
            }

            for (int i = 1; i < command.Length; i++)
            {
                advFeature.Args.Add(command[i]);
            }

            if (!advFeature.IsValidArgumentCount(out int expected))
            {
                throw new Exception(
                    $"Invalid argument count for command {command[0]}: {advFeature.Args.Count} (expected {expected})");
            }

            advFeatures.Add(advFeature);
        }

        return advFeatures;
    }
    public Character GetCharacterForCmd(string arg)
    {
        if(UseCharacterNames)
        {
            if (NameToID.TryGetValue(arg, out int id))
            {
                return NJBJIIIACEP.OAAMGFLINOB[id].EMDMDLNJFKP;
            }
            return NJBJIIIACEP.OAAMGFLINOB[0].EMDMDLNJFKP;
        }
        return UnmappedPromo.BBPPMGDKCBJ[int.Parse(arg)];
    }

    public static PromoData CreatePromo(string file)
    {
        List<string> lines = File.ReadAllLines(file).ToList();
        return FromString(lines);
    }
}