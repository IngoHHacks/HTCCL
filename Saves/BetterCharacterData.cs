using Newtonsoft.Json;
using System.Reflection;
using HTCCL.Content;
// ReSharper disable InconsistentNaming
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace HTCCL.Saves;

internal class BetterCharacterData
{
    public int? absent;

    public int? age;

    public int? agreement;

    public float? angle;

    public int? anim;

    public float? armMass;

    public float? bodyMass;

    public int? cell;

    public int? chained;

    public BetterCostumeData[] costumeC;

    public int? cuffed;

    public int? dead;

    public float? demeanor;

    public int? gender;

    public int? grudge;

    public float? headSize;

    public float? health;
    
    public float? healthLimit;

    public float? height;

    public int? home;

    public int? id;

    public int? injury;

    public int? injuryTime;

    public float? legMass;

    public int? location;

    public int?[] moveAttack = new int?[6];

    public int?[] moveBack = new int?[9];

    public int?[] moveCrush = new int?[6];

    public int?[] moveFront = new int?[17];

    public int?[] moveGround = new int?[7];

    public float? muscleMass;

    public string musicC;

    public float? musicSpeed;

    public string name;

    public int? negotiated;

    public int? news;

    public float?[] oldStat = new float?[7];
    
    public int? platform;

    public int? player;

    public int? possessive;
    
    public int? pregnant;

    public int? promo;

    public int? promoVariable;

    public int? prop;

    public string[] relationC = new string[7];

    public int? role;

    public int?[] scar = new int?[17];

    public int? seat;

    public float? spirit;

    public int? stance;

    public float?[] stat = new float?[7];

    public int?[] taunt = new int?[4];

    public int? tauntHandshake;

    public int? tauntWave;

    public int? team;

    public string teamName;

    public int? toilet;

    public float? voice;

    public int? worked;

    public int? warrant;

    public int? warrantVariable;

    public int? warrantVictim;

    public int? warrantWitness;

    public float? x;

    public float? y;

    public float? z;
    
    public string VERSION = "1.0.0-HT";

    public static BetterCharacterData FromRegularCharacter(Character character, Character[] allCharacters, bool ignoreRelations = false)
    {
        BetterCharacterData bcd =
            JsonConvert.DeserializeObject<BetterCharacterData>(JsonConvert.SerializeObject(character))!;
        bcd.costumeC = new BetterCostumeData[character.costume.Length];
        for (int i = 0; i < character.costume.Length; i++)
        {
            bcd.costumeC[i] = BetterCostumeData.FromRegularCostumeData(character.costume[i]);
        }

        bcd.relationC = new string[character.relation.Length];
        if (!ignoreRelations) {
            for (int i = 0; i < character.relation.Length; i++)
            {
                if (i == 0)
                {
                    bcd.relationC[i] = "0";
                    continue;
                }
                if (character.relation[i] >= allCharacters.Length)
                {
                    bcd.relationC[i] = "0";
                    continue;
                }

                bcd.relationC[i] = character.relation[i] == 0
                    ? "0"
                    : allCharacters[i].name + "=" + character.relation[i];
            }
        } else {
            for (int i = 0; i < character.relation.Length; i++)
            {
                bcd.relationC[i] = "0";
            }
        }

        bcd.grudge = 0;

        return bcd;
    }

    public Character ToRegularCharacter(Character[] allCharacters)
    {
        Character character = JsonConvert.DeserializeObject<Character>(JsonConvert.SerializeObject(this),
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })!;
        character.costume = new Costume[this.costumeC.Length];
        for (int i = 0; i < this.costumeC.Length; i++)
        {
            if (this.costumeC[i] == null)
            {
                character.costume[i] = null;
                continue;
            }
            this.costumeC[i].charID = character.id;

            character.costume[i] = this.costumeC[i].ToRegularCostume();
        }

        character.relation = new int[Characters.no_chars + 2];
        if (this.VERSION.EndsWith("-HT"))
        {
            for (int i = 0; i < this.relationC.Length; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                if (this.relationC[i] == "0")
                {
                    continue;
                }
                string[] split = this.relationC[i].Split('=');
                string name = split[0];
                try
                {
                    var id = allCharacters.Single(c => c != null && c.name != null && c.name == name).id;
                    character.relation[id] = int.Parse(split[1]);
                }
                catch (Exception)
                {
                    if (i >= character.relation.Length)
                    {
                        LogWarning("Failed to find character with name " + name + ", skipping because id is out of bounds.");
                        continue;
                    }
                    character.relation[i] = int.Parse(split[1]);
                    LogWarning("Failed to find character with name " + name + ", using id instead.");
                
                }
            }
        }
        character.grudge = 0;
        character.team = 0;

        return character;
    }

    public void MergeIntoCharacter(Character character)
    {
        foreach (FieldInfo field in typeof(BetterCharacterData).GetFields())
        {
            if (field.FieldType.IsArray)
            {
                Array array = (Array)field.GetValue(this);
                if (array == null)
                {
                    continue;
                }

                bool allNull = true;
                bool allNonNull = true;
                foreach (object element in array)
                {
                    if (element != null)
                    {
                        allNull = false;
                    }
                    else
                    {
                        allNonNull = false;
                    }
                }

                if (allNull)
                {
                    field.SetValue(this, null);
                }
                else if (!allNonNull)
                {
                    throw new Exception("It is not possible to merge arrays with both null and non-null elements.");
                }
            }
        }

        // Ignore nulls and nulls in arrays
        JsonConvert.PopulateObject(JsonConvert.SerializeObject(this), character,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        character.grudge = 0;
        character.team = 0;
    }
}