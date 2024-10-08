﻿namespace HTCCL.API;
public class CustomMatch
{
    internal static Dictionary<string, int> CustomPresetsPos = new();
    internal static Dictionary<string, int> CustomPresetsNeg = new();
    internal static Dictionary<string, int> CustomCagesPos = new();
    internal static Dictionary<string, int> CustomCagesNeg = new();
    internal static Dictionary<string, int> CustomRewardsPos = new();
    internal static Dictionary<string, int> CustomRewardsNeg = new();

    /// <summary>
    /// <para>Use this to register your custom match preset and get a preset ID.</para>
    /// <para>string Name - name identifier.</para>
    /// <para>bool PositiveValue - "true" to add to the positive end of the list, "false" to add to the negative end instead.</para>
    /// </summary>
    /// <param name="Name">Name identifier</param>
    /// <param name="PositiveValue">"true" to add to the positive end of the list, "false" to add to the negative end instead.</param>
    /// <returns>The ID of your custom preset, null if it fails.</returns>
    public static int? RegisterCustomPreset(string Name, bool PositiveValue)
    {
        int value;
        if (PositiveValue)
        {
            if (CustomPresetsPos.TryGetValue(Name, out value))
            {
                LogWarning(Name + " is already registered as preset " + value);
            }
            else
            {
                value = ++MappedMatch.no_presets;
                LogInfo("REGISTERED " + Name + " as preset " + value);
                CustomPresetsPos.Add(Name, value);
            }
            return value;
        }
        else
        {
            if (RegisterHardcodedElement(Name, CustomPresetsNeg, "Preset", out value))
            {
                LogInfo("REGISTERED " + Name + " as preset " + -value);
                CustomPresetsNeg.Add(Name, -value);
            }
            return -value;
        }
    }
    private static bool RegisterHardcodedElement(string Name, Dictionary<string, int> dictionary, string type, out int pos)
    {
        if (dictionary.TryGetValue(Name, out pos))
        {
            LogWarning(Name + " is already registered as " + type + " " + pos);
            return false;
        }
        pos = 10001 + dictionary.Count;
        return true;
    }
    /// <summary>
    /// <para>Use this to register your custom cage and get a cage ID.</para>
    /// <para>string Name - name identifier.</para>
    /// <para>bool PositiveValue - "true" to add to the positive end of the list, "false" to add to the negative end instead.</para>
    /// </summary>
    /// <param name="Name">Name identifier</param>
    /// <param name="PositiveValue">"true" to add to the positive end of the list, "false" to add to the negative end instead.</param>
    /// <returns>The ID of your custom cage, null if it fails.</returns>
    public static int? RegisterCustomCage(string Name, bool PositiveValue)
    {
        int value;
        if (PositiveValue)
        {
            if (RegisterHardcodedElement(Name, CustomCagesPos, "Cage", out value))
            {
                LogInfo("REGISTERED " + Name + " as cage " + value);
                CustomCagesPos.Add(Name, value);
            }
            return value;
        }
        else
        {
            if (RegisterHardcodedElement(Name, CustomCagesNeg, "Cage", out value))
            {
                LogInfo("REGISTERED " + Name + " as cage " + -value);
                CustomCagesNeg.Add(Name, -value);
            }
            return -value;
        }
    }

    public static int? RegisterCustomReward(string Name, bool PositiveValue)
    {
        int value;
        if (PositiveValue)
        {
            if (RegisterHardcodedElement(Name, CustomRewardsPos, "Reward", out value))
            {
                LogInfo("REGISTERED " + Name + " as reward " + value);
                CustomRewardsPos.Add(Name, value);
            }
            return value;
        }
        else
        {
            if (RegisterHardcodedElement(Name, CustomRewardsNeg, "Reward", out value))
            {
                LogInfo("REGISTERED " + Name + " as reward " + -value);
                CustomRewardsNeg.Add(Name, -value);
            }
            return -value;
        }
    }
}
