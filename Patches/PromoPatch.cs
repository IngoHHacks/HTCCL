using System.Text.RegularExpressions;
using HTCCL.API;
using HTCCL.Content;

namespace HTCCL.Patches;

[HarmonyPatch]
internal class PromoPatch
{
    
    /*
     * Patch:
     * - Runs promo script of custom promos.
     */
    [HarmonyPatch(typeof(UnmappedPromo), nameof(UnmappedPromo.KMFBNADPGMF))]
    [HarmonyPrefix]
    public static void Promo_KMFBNADPGMF()
    {
        int promoId = UnmappedPromo.LODPJDDLEKI - PromoData.BASE_PROMO_OFFSET;
        if (promoId < 0)
        {
            return;
        }

        PromoData promo;
        PromoLine promoLine;
        if (promoId >= PromoData.CODE_PROMO_OFFSET - PromoData.BASE_PROMO_OFFSET) {
            promo = CodePromoManager.GetPromoData(promoId - PromoData.CODE_PROMO_OFFSET + PromoData.BASE_PROMO_OFFSET);
            promoLine = CodePromoManager.HandlePage(promoId - PromoData.CODE_PROMO_OFFSET + PromoData.BASE_PROMO_OFFSET);
            if (promoLine == null)
            {
                MappedPromo.script = 0;
                return;
            }
        }
        else
        {
            promo = CustomPromoData[promoId];
            int page = UnmappedPromo.ODOAPLMOJPD - 1;
            if (page >= promo.NumLines)
            {
                MappedPromo.script = 0;
                return;
            }
            promoLine = promo.PromoLines[page];
        }

        if (promo.UseCharacterNames)
        {
            if (!promo.NameToID.TryGetValue(promoLine.FromName, out int fromid))
            {
                fromid = 0;
            }
            if (!promo.NameToID.TryGetValue(promoLine.ToName, out int toid))
            {
                toid = 0;
            }
            ExecutePromoLine(promoLine.Line1, promoLine.Line2, fromid,
                toid, promoLine.Demeanor, promoLine.TauntAnim, true);
        }
        else
        {
            ExecutePromoLine(promoLine.Line1, promoLine.Line2, promoLine.From,
                promoLine.To, promoLine.Demeanor, promoLine.TauntAnim, false);
        }

        if (UnmappedPromo.IMJHCHECCED >= 100f && UnmappedPromo.KJPJODMIPGO < UnmappedPromo.ODOAPLMOJPD)
        {
            if (promoLine.Features != null)
            {
                foreach (AdvFeatures feature in promoLine.Features)
                {
                    AdvFeatures.CommandType cmd = feature.Command;
                    switch (cmd)
                    {
                        case AdvFeatures.CommandType.SetRealEnemy:
                            promo.GetCharacterForCmd(feature.Args[0])
                                .DADEOGCFAAN(promo.GetCharacterForCmd(feature.Args[1]).id, -1, 0);
                            break;
                        case AdvFeatures.CommandType.SetStoryEnemy:
                            promo.GetCharacterForCmd(feature.Args[0])
                                .DADEOGCFAAN(promo.GetCharacterForCmd(feature.Args[1]).id, -1);
                            break;
                        case AdvFeatures.CommandType.SetRealFriend:
                            promo.GetCharacterForCmd(feature.Args[0])
                                .DADEOGCFAAN(promo.GetCharacterForCmd(feature.Args[1]).id, 1, 0);
                            break;
                        case AdvFeatures.CommandType.SetStoryFriend:
                            promo.GetCharacterForCmd(feature.Args[0])
                                .DADEOGCFAAN(promo.GetCharacterForCmd(feature.Args[1]).id, 1);
                            break;
                        case AdvFeatures.CommandType.SetRealNeutral:
                            promo.GetCharacterForCmd(feature.Args[0])
                                .DADEOGCFAAN(promo.GetCharacterForCmd(feature.Args[1]).id, 0, 0);
                            break;
                        case AdvFeatures.CommandType.SetStoryNeutral:
                            promo.GetCharacterForCmd(feature.Args[0])
                                .DADEOGCFAAN(promo.GetCharacterForCmd(feature.Args[1]).id, 0);
                            break;
                        case AdvFeatures.CommandType.PlayAudio:
                            if (feature.Args[0] == "-1")
                            {
                                UnmappedSound.KIKKPCJGDLM(UnmappedPromo.NNMDEFLLNBF, -1, 1f);
                            }
                            else
                            {
                                UnmappedSound.AFLOJPBKNFB.PlayOneShot(
                                    UnmappedSound.JOPLJHCBICG[Indices.ParseCrowdAudio(feature.Args[0])], 1);
                            }

                            break;
                    }
                }
            }

            UnmappedPromo.KJPJODMIPGO = UnmappedPromo.ODOAPLMOJPD;
        }
    }
    
#pragma warning disable Harmony003
    private static void ExecutePromoLine(string line1, string line2, int from, int to, float demeanor, int taunt, bool useNames)
    {
        line1 = ReplaceVars(line1);
        line2 = ReplaceVars(line2);
        if(useNames)
        {
            UnmappedPromo.LIHAMAIBHFN(from, to, demeanor, taunt);
        }
        else
        {
            UnmappedPromo.LIHAMAIBHFN(UnmappedPromo.ACELMDKGHEK[from], UnmappedPromo.ACELMDKGHEK[to], demeanor, taunt);
        }

        UnmappedPromo.MLLPFEKAONO[1] = line1;
        UnmappedPromo.MLLPFEKAONO[2] = line2;
    }

    private static string ReplaceVars(string line)
    {
        // Replace $variables
        MatchCollection matches = Regex.Matches(line, @"\$\$?([a-zA-Z]+)(\W|$)");
        foreach (Match match in matches)
        {
            try
            {
                string varName = match.Groups[1].Value.ToLower();
                string varValue = varName switch
                {
                    "location" => MappedWorld.DescribeLocation(World.location),
                    "date" => "Day " + MappedProgress.day,
                    _ => "UNKNOWN"
                };
                line = line.Replace(match.Value, varValue + match.Groups[2].Value);
            }
            catch (Exception e)
            {
                line = line.Replace(match.Value, "INVALID");
                LogError(e);
            }
        }
        matches = Regex.Matches(line, @"\$\$?([a-zA-Z]+)-?(\d+)(\W|$)");
        foreach (Match match in matches)
        {
            try
            {
                string varName = match.Groups[1].Value.ToLower();
                int varIndex = int.Parse(match.Groups[2].Value);
                string varValue;
                if (varName == "date")
                {
                    var date = MappedProgress.day + varIndex;
                    varValue = "Day " + date;
                }
                else
                {
                    varValue = varName switch
                    {
                        "name" => MappedPromo.c[varIndex].name,
                        "prop" => MappedWeapons.Describe(MappedPromo.c[varIndex].prop),
                        "team" => MappedPromo.c[varIndex].teamName,
                        _ => "UNKNOWN"
                    };
                }
                line = line.Replace(match.Value, varValue + match.Groups[3].Value);
            }
            catch (Exception e)
            {
                line = line.Replace(match.Value, "INVALID");
                LogError(e);
            }
        }
        matches = Regex.Matches(line,
            @"\$\$?([a-zA-Z]+)-?(\d+)_-?(\d+)(\W|$)");
        foreach (Match match in matches)
        {
            try
            {
                string varName = match.Groups[1].Value.ToLower();
                int varIndex1 = int.Parse(match.Groups[2].Value);
                int varIndex2 = int.Parse(match.Groups[3].Value);
                string varValue = varName switch
                {
                    "movefront" => MappedAnims.DescribeMove(MappedPromo.c[varIndex1].moveFront[varIndex2]),
                    "moveback" => MappedAnims.DescribeMove(MappedPromo.c[varIndex1].moveBack[varIndex2]),
                    "moveground" => MappedAnims.DescribeMove(MappedPromo.c[varIndex1].moveGround[varIndex2]),
                    "moveattack" => MappedAnims.DescribeMove(MappedPromo.c[varIndex1].moveAttack[varIndex2]),
                    "movecrush" => MappedAnims.DescribeMove(MappedPromo.c[varIndex1].moveCrush[varIndex2]),
                    "taunt" => ((MappedTaunt) MappedAnims.taunt[MappedPromo.c[varIndex1].taunt[varIndex2]]).name,
                    "stat" => MappedPromo.c[varIndex1].stat[varIndex2].ToString("0"),
                    _ => "UNKNOWN"
                };

                line = line.Replace(match.Value, varValue + match.Groups[4].Value);
            }
            catch (Exception e)
            {
                line = line.Replace(match.Value, "INVALID");
                LogError(e);
            }
        }

        matches = Regex.Matches(line, @"@([a-zA-Z]+)(\d+)(\W|$)");
        foreach (Match match in matches)
        {
            string varName = match.Groups[1].Value;
            int varIndex = int.Parse(match.Groups[2].Value);
            string varValue = UnmappedPromo.BBPPMGDKCBJ[varIndex].POOMHHMDABP(varName);

            line = line.Replace(match.Value, varValue + match.Groups[3].Value);
        }

        return line;
    }
#pragma warning restore Harmony003
}