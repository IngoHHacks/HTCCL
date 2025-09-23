using HTCCL.API.Events;
using HTCCL.Saves;

namespace HTCCL.Utils;

public class CharacterUtils
{
    
    public static void SaveAsBackup(int id)
    {
        NonSavedData.DeletedCharacters.Add(Characters.c[id]);
    }
    
    public static void DeleteCharacter(int id)
    {
        if (id < 1 || id >= Characters.c.Length)
        {
            throw new IndexOutOfRangeException($"Character ID {id} is out of range!");
        }

        var delC = Characters.c[id];
        CharacterEvents.InvokeBeforeCharacterRemoved(id, delC);
        try
        {
            SaveAsBackup(id);

            if (Characters.star == id)
            {
                Characters.star = 1;
            }
            else if (Characters.star > id)
            {
                Characters.star--;
            }
            
            if (Progress.missionClient == id)
            {
                Progress.missionClient = 1;
            }
            else if (Progress.missionClient > id)
            {
                Progress.missionClient--;
            }

            if (Progress.missionTarget == id)
            {
                Progress.missionTarget = 1;
            }
            else if (Progress.missionTarget > id)
            {
                Progress.missionTarget--;
            }
            
            for (int i = 1; i < Progress.hiChar.Length; i++)
            {
                if (Progress.hiChar[i] == id)
                {
                    Progress.hiChar[i] = 1;
                }
                else if (Progress.hiChar[i] > id)
                {
                    Progress.hiChar[i]--;
                }
            }

            for (int i = 1; i < MappedItems.stock.Length; i++)
            {
                if (MappedItems.stock[i] == null)
                {
                    continue;
                }

                if (MappedItems.stock[i].owner == id)
                {
                    MappedItems.stock[i].owner = 1;
                }
                else if (MappedItems.stock[i].owner > id)
                {
                    MappedItems.stock[i].owner--;
                }
            }

            for (int i = 1; i < MappedWeapons.stock.Length; i++)
            {
                if (MappedWeapons.stock[i] == null)
                {
                    continue;
                }

                if (MappedWeapons.stock[i].owner == id)
                {
                    MappedWeapons.stock[i].owner = 1;
                }
                else if (MappedWeapons.stock[i].owner > id)
                {
                    MappedWeapons.stock[i].owner--;
                }

                if (MappedWeapons.stock[i].holder == id)
                {
                    MappedWeapons.stock[i].holder = 1;
                }
                else if (MappedWeapons.stock[i].holder > id)
                {
                    MappedWeapons.stock[i].holder--;
                }
            }

            foreach (var character in Characters.c)
            {
                if (character == null)
                {
                    continue;
                }

                for (int i = 1; i < character.relation.Length - 1; i++)
                {
                    if (i >= id) {
                        character.relation[i] = character.relation[i+1];
                    }
                }

                for (int i = 1; i < character.costume.Length; i++)
                {
                    if (character.costume[i] == null)
                    {
                        continue;
                    }

                    if (character.costume[i].charID > id)
                    {
                        character.costume[i].charID--;
                    }
                }

                if (character.grudge == id)
                {
                    character.grudge = 1;
                }
                else if (character.grudge > id)
                {
                    character.grudge--;
                }
                if (character.team == id)
                {
                    character.team = 1;
                }
                else if (character.team > id)
                {
                    character.team--;
                }
            }
            
            for (int i = id; i < Characters.c.Length - 1; i++)
            {
                Characters.c[i] = Characters.c[i + 1];
                Characters.c[i].id = i;
            }

            for (int i = id; i < Progress.charUnlock.Length - 1; i++)
            {
                Progress.charUnlock[i] = Progress.charUnlock[i + 1];
            }

            Characters.no_chars--;
            Array.Resize(ref Characters.c, Characters.no_chars + 1);
            Array.Resize(ref Progress.charUnlock, Characters.no_chars + 1);
            Array.Resize(ref GLPGLJAJJOP.APPDIBENDAH.savedChars, Characters.no_chars + 1);
            Array.Resize(ref GLPGLJAJJOP.APPDIBENDAH.charUnlock, Characters.no_chars + 1);
            for (int i = 1; i <= Characters.no_chars; i++)
            {
                if (Characters.c[i] == null)
                {
                    continue;
                }
                Array.Resize(ref Characters.c[i].relation, Characters.no_chars + 1);
            }
            CharacterEvents.InvokeAfterCharacterRemoved(id, delC);
        }
        catch (Exception e)
        {
            CharacterEvents.InvokeAfterCharacterRemovedFailure(id, Characters.c[id]);
            throw new Exception($"Failed to delete character {id} ({Characters.c[id].name})!", e);
        }
    }

    public static int CreateRandomCharacter()
    {
        try
        {
            var character = MappedCharacters.CopyClass(Characters.c[1]);
            CharacterEvents.InvokeBeforeCharacterAdded(Characters.no_chars + 1, character);
            Characters.no_chars++;
            Array.Resize(ref Characters.c, Characters.no_chars + 1);
            Array.Resize(ref Progress.charUnlock, Characters.no_chars + 1);
            Array.Resize(ref GLPGLJAJJOP.APPDIBENDAH.savedChars, Characters.no_chars + 1);
            Array.Resize(ref GLPGLJAJJOP.APPDIBENDAH.charUnlock, Characters.no_chars + 1);
            for (int i = 1; i <= Characters.no_chars; i++)
            {  
                if (Characters.c[i] == null)
                {
                    continue;
                }
                Array.Resize(ref Characters.c[i].relation, Characters.no_chars + 1);
            }
            Progress.charUnlock[Characters.no_chars] = 1;
            Characters.c[Characters.no_chars] = character;
            ((MappedCharacter)Characters.c[Characters.no_chars]).Generate(Characters.no_chars);
            ((MappedCharacter)Characters.c[Characters.no_chars]).teamName = "";
            foreach (var c in Characters.c)
            {
                if (c == null)
                {
                    continue;
                }
            }
            CharacterEvents.InvokeAfterCharacterAdded(Characters.no_chars, Characters.c[Characters.no_chars]);
            LogInfo($"New character created: {((MappedCharacter)Characters.c[Characters.no_chars]).name}");
            return Characters.no_chars;
        }
        catch (Exception e)
        {
            CharacterEvents.InvokeAfterCharacterAddedFailure(Characters.no_chars, Characters.c[Characters.no_chars]);
            throw new Exception($"Failed to create random character {Characters.no_chars}!", e);
        }
    }
}