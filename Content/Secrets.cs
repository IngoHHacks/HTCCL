using HTCCL.Saves;
using Random = UnityEngine.Random;

namespace HTCCL.Content;

internal static class Secrets
{
    // Easter eggs
    // Feel free to add your own easter eggs here!
    // Please keep them appropriate, though.
    // Please also keep them in alphabetical order.
    // Easter eggs are randomly selected from this list, 1/100 chance to be printed to the console when the mod is loaded.
    public static readonly string[] EasterEggs =
    {
        ":egg:",
        ":eggplant:",
        ":eyes:",
        ":flushed:",
        ":ok_hand:",
        "A dog by the name of Ingo appears. He looks at you, then speaks \"Please don't use this mod. It's bad.\" He then disappears.",
        "A fox walks into a bar.",
        "A wild Mat Dickie appears! He uses \"Bad Code\"! It's super effective!",
        "Ah, I love the smell of game updates that break mods in the morning.",
        "Apparently, Ingo is a \"very nice person\". I don't believe that.",
        "Attention all HTCCL gamers: Ingo is in great danger and he needs your help to wipe out all the bugs in Mat Dickie's code. But to do this, he needs a deobfuscation mappings and a couple of dog treats. To help him, all he needs is your credit card number, the three digits on the back, and the expiration month and year. But you gotta be quick so that Ingo can secure the information and achieve the epic victory royal!",
        "Average programmer writes 600,000,000,000 lines of bad code per year factoid actually just statistical error. Average programmer writes 0 lines of bad code per year. Mat Dickie, who lives in cave & writes over 50,000,000,000,000 each day, is an outlier adn should not have been counted.",
        "Behind every person stands, eventually, due to the rounding of the earth, that same person, looking over their shoulder, stealing their ideas.",
        "Beware of Mat Dickie. He's watching you. Always. Everywhere.",
        "Can people stop making fun of Mat Dickie? He's trying his best. He's just not very good at it.",
        "Can someone tell Mat Dickie to make a game that doesn't have garbage code? Thanks.",
        "Cats everywhere are now wearing egg shells. It's the new fashion trend.",
        "Ceci n'est pas une oeuf de Pâques.",
        "Despite Mat Dickie's code, this game is actually pretty good.",
        "Did you know that 75% of statistics are made up on the spot?",
        "Did you know that HTCCL is secretly a recursive acronym? It stands for \"HTCCL Tramples Code Complexity Lazily\".",
        "Dit is geen paasei.",
        "Do you hate HTCCL? If so, remember to leave a dislike and a hate comment. Your feedback is greatly appreciated.",
        "Does Ingo even check #bug-reports anymore?",
        "Dogs love egg shells. It's a fact.",
        "Don't forget to wash your hands after playing this game. That's an order!",
        "Friendship ended with Mat Dickie. Now Ingo is my best friend.",
        "GamingMaster just told Mat Dickie that his code is bad. Mat Dickie: \"No u.\"",
        "GamingMaster wrote another pull request for HTCCL. IngoH: \"Wow, that's a lot of code!\" GamingMaster: \"Yeah, I know. I'm a good programmer.\"",
        "Ha! You just wasted your one in a thousand chance on this easter egg! Sucker!",
        "Hello, player. I'd like to play a game. You have been trapped in a room with a computer that has Hard Time III installed on it. The only way to escape is beat every single character in the game in every single court case type. If you fail, you will be locked in the room forever. Good luck.",
        "Hey, Vsauce, Ingo here. Do you know what a Hard Time III is? You'll probably say something like \"Of course I do, Ingo, I'm not an idiot.\" But what if I told you that you're wrong?",
        "Hey, you. You're finally awake. You were trying to play modded Hard Time III, right? Walked right into that Mat Dickie ambush, same as us, and that prisoner over there.",
        "Historian: \"And here we have the ancient code of Mat Dickie. It's said that this code was so bad, it caused the fall of the Roman Empire.\"",
        "How do you delete a Mat Dickie? Asking for a friend.",
        "How many easter eggs can Ingo put in this mod before people will stop taking him seriously? Apparently, the answer is \"a lot\".",
        "I typed too much. My computer exploded. Help.",
        "I visited the modding server of Hard Time III to have some friendly chat. At that point, I was trolled by a user named \"Mat Dickie\".",
        "I'm a dog. Woof woof. I wear egg shells. That's my thing.",
        "I'm a good programmer - Mat Dickie, probably.",
        "I'm going to hack Mat's code and make it so that every time you use Hard Time III, it automatically downloads HTCCL. - IngoH, probably.",
        "I'm going to hug you. And there's nothing you can do about it.",
        "If you ever feel like you're bad at coding, just remember that Mat Dickie exists.",
        "If you listen closely, you can hear the sound of Mat Dickie's code breaking.",
        "If you look closely, you can see a zero in the code. That's the amount of attention Mat Dickie pays to his code.",
        "If you run into a bug with this mod, please report it to Mat Dickie. He will not fix it, but it will be funny. He will probably ban you from his mailing list, but that's a small sacrifice to make for the sake of comedy.",
        "If you're reading this, you can read.",
        "Imagine if this game had a story mode. Oh wait, it does.",
        "Ingo is not responsible for any damage caused by this mod. This includes, but is not limited to: broken game saves, broken game files, broken game, broken computer, nightmares, vomiting, increased risk of heart attack, increased risk of cancer, increased risk of being eaten by a giant spider, and death.",
        "Ingo says \"hi\".",
        "Ingo sus.",
        "Ingo, if you're reading this, your egg shell hat is stupid. Like, really stupid.",
        "IngoH: \"One day, I will delete Mat Dickie's code and replace it with my own.\" Also IngoH: *eats dog treat*",
        "Introducing: HTCCL Lite! Same content loader, but zero calories!",
        $"It is the year {DateTime.Now.Year + 450}. Mat Dickie pushed out another update for Hard Time III that broke all the mods. Ingo is still trying to fix his mod. He has been trying to fix it for 450 years. He is still not done.",
        "It's dangerous to go alone! Take this: a copy of Hard Time III.",
        "It's said that if you say \"HTCCL\" three times in front of a mirror, you will be visited by the ghost of Mat Dickie.",
        "Let's play a game. In front of you, there is a button. The button will make Mat Dickie become real. It will also make him come to your house and break your computer. Do you press the button?",
        "Let's play a game. It's called \"Mat Breaks Mods\". It's a very fun game.",
        "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM Oh sorry, wrong textbox.",
        "Man tries installing HTCCL to mod Hard Time III. What happens next will shock you!",
        "Mat Dickie wrote another essay about how he's the best game developer in the world. The essay is 50,000 pages long and the word \"best\" appears over 9000 times.",
        "Mat Dickie's game reminds me of the time I was in a court case with him. I was about to win, but then he pulled out a gun and shot me. I died. The end.",
        $"Mat Stop Breaking Mods Challenge {DateTime.Now.Year} (IMPOSSIBLE?) (GONE WRONG) (GONE SEXUAL) (GONE MAT DICKIE) (SUBSCRIBE TO INGOH) (NOT CLICKBAIT) (POLICE CALLED)",
        "Mat, if you're reading this, please stop breaking my mod. Thanks.",
        "Mat, this is your conscience speaking. Your conscience is telling you to make the deobfuscation mappings public.",
        "Never gonna give you up, never gonna let you down, never gonna run around and desert you. Never gonna make you cry, never gonna say goodbye, never gonna tell a lie and hurt you.",
        "No, mom. I'm not playing games. I'm doing important work. I'm writing easter eggs for a mod.",
        "Nobody: Mat Dickie: *writes bad code*",
        "OMG, I just realized! Mat Dickie isn't actually real! He's just a character made up by Ingo to make it look like he's not the one to blame for all the bugs in this mod!",
        "OOoooOOOOOoooOOOOOOooo! I'm the infamous ghost of Mat Dickie! I'm here to haunt this mod!",
        "Oh no, I'm running out of ideas for these easter eggs.",
        "One day, Mat Dickie will make a good game. That day is not today though.",
        "Oops, I accidentally the entire mod.",
        "Oops, I accidentally wrote this easter egg twice. Oh well, I'm too lazy to fix it.",
        "Other languages await... I need to become a programmer.",
        "OwO, what's this? *notices your easter egg*",
        "People say that Mat Dickie is a bad programmer. I disagree. He's not a bad programmer. He's a terrible programmer.",
        "Ph'nglui mglw'nafh MDickie R'lyeh wgah'nagl fhtagn.",
        "Please, take a moment to appreciate the fact that you are reading this easter egg. It's a rare occurrence, and you should cherish it.",
        "Press Alt+F4 to unlock \"Mat Dickie Mode\". This mode will you view the game from Mat Dickie's perspective. It's a very fun mode.",
        "Press F to pay respects to Mat Dickie. He's dead, by the way. In fact, he's been dead for years. I'm just pretending to be him.",
        "Psst, don't tell Ingo, but I'm actually a spy for Mat Dickie. I'm here to sabotage this mod.",
        "Ram Ranch really rocks. Hold on, wrong list.",
        "Shoutouts to SimpleFlips.",
        "So, you're approaching me? Instead of running away, you're coming right to me? Even though your grandfather, Mat Dickie, told you the secret of The Code like an exam student scrambling to finish the problems on an exam until the last moments before the chime?",
        "Sometimes I wonder if Mat Dickie is actually a good programmer, and he's just pretending to be bad at coding to troll us.",
        "Sorry, but HTCCL is actually a dating sim. You've been playing it wrong this whole time.",
        "Static obfuscation is killing the modding community, says mod developer; \"Nowadays, we don't even have to fix our mods after every update anymore. It's just not how it used to be.\"",
        "Street's cat now has a cameo in the game. It's hidden somewhere. Good luck finding it.",
        "TODO: Street, please remove this easter egg before release. Thanks.",
        "Take the 'L', bozo. Yes, I'm talking to you, the person reading this. You're a bozo. Not even a clown, just a bozo.",
        "That's a lot of mods you have installed. The developers are of HTCCL are impressed. Due to the nature of HTCCL, any comments about the amount of mods you have installed are speculation on our part. Please disregard this message if you do not have a lot of mods installed.",
        "That's a nice mod you have there. It would be a shame if someone... deleted it.",
        "The Holy trinity of Mat Dickie; fun, features, and frustration. These blessings were so strong, Mat restricted their power. He said, \"I will give you three choices. You can have fun, but you will have no features. You can have features, but you will have no fun. Or you can have both, but you will have to deal with frustration.\" Then, as humanity chose, a crack broke the earth. A dog rose from the depths of the earth, and said, \"I will offer the powerful choice of mod support! I am IngoH, lord of all Undickie.\" Humanity rose and said \"Begone, dog! We will not be tempted by your foul heresy!\" And Mat rose as well, and smote IngoH with a block of code so complex, it was said to contain all the bugs in the universe. IngoH cried \"This will not be the last of me! Mat will betr-\" and he fell into the eternal Abyss of deprecated code. Mat gifted humanity with a new update, a monumental piece of software that brimmed with fun, features, and above all, frustration - yes, a frustration so profound that it transcended the mundane and approached the divine. And humanity journeyed off with their new update, as Ingo's words echoed through their heads.",
        "The code is a lie.",
        "The real HTCCL is the friends we made along the way.",
        "This game is not a prison game. It's a life simulator.",
        "This game may contain jumpscares. Not because it's a horror game, but because Mat Dickie is so bad at coding that he probably accidentally wrote some code that makes the game jumpscare you.",
        "This is a placeholder easter egg. Please come back later.",
        "This is not an easter egg. It's a feature.",
        "To be fair, you have to have a very high IQ to understand HTCCL. The code is extremely subtle, and without a solid grasp of theoretical physics most of the code will go over a typical programmer's head.",
        "To mod or not to mod, that is the question.",
        "Touch grass. Now.",
        "Turns out Mat Dickie never existed. He was just a figment of your imagination.",
        "Turns out Mat's new game, Hard Time III, is just a reskin of Hard Time III. Who would've thought?",
        "Turns out Mat's new game, Old School, is just a reskin of Hard Time III. Who would've thought?",
        "Uh oh, stinky. That's what I say whenever I look at Mat Dickie's code.",
        "UwU, what's this? *notices your code*",
        "HTCCL 2: Electric Boogaloo - Coming soon to a Steam Workshop near you.",
        "HTCCL DELUXE: IngoH Deletes Mat Dickie's Code - Coming soon to a Steam Workshop near you.",
        "HTCCL II: The Prisoners Strike Back - Coming soon to a Steam Workshop near you.",
        "HTCCL PLUS: Mat Dickie's Revenge - Coming soon to a Steam Workshop near you.",
        "HTCCL Terms of Service: By using this mod, you agree to give Ingo all your money. You also agree to give Ingo all your belongings, including your house, your car, your family, your pets, and your soul. Furthermore, Ingo will be allowed to use your belongings in any way he wants, including but not limited to: selling them, destroying them, eating them, and using them to build a giant statue of himself. If you do not agree to these terms, you are not allowed to use this mod.",
        "HTCCL is a mod for Hard Time III. But what if I told you that Hard Time III is actually a mod for HTCCL? Think about it.",
        "HTCCL is a mod. I don't know what that means, but it sounds cool.",
        "HTCCL is a virus, by the way. Ingo made it to steal your money to buy dog treats.",
        "HTCCL is made by some idiot named Ingo and a few other idiots.",
        "HTCCL: The Movie: Mat Dickie F*cking Dies - Coming soon to a movie theater near you.",
        "Wait no, don't click that \"Disable Easter Eggs\" button!",
        "Watch out, watch out, watch out. BOOM! RKO! Outta nowhere!",
        "Weather forecast for today: There is a 100% chance of weather.",
        "What if... we kissed... in the Hard Time III ring... haha, just kidding... unless?",
        "What's a Mat Dickie? And why is it breaking my mod?",
        "Why are there no dinosaurs in Hard Time III? It's because Mat Dickie himself is a dinosaur. His ancient code is proof of that.",
        "Why do they call it a HTCCL when you can't even clc?",
        "Why is Ingo writing these easter eggs? He should be working on the mod instead.",
        "Working with Mat Dickie's code is like trying to find a needle in a haystack. While blindfolded. And the haystack is on fire.",
        "Hard Time III is actually a roguelike. If you die in the game, you die in real life! Haha, just kidding. Or am I?",
        "Yo, what's up, it's ya boi, IngoH, back at it again with another video. Don't forget to SMASH that like and subscribe button, and hit that bell icon to get notified whenever I upload a new video.",
        "You just lost the game.",
        "Your feedback is important to us. If you have any, please shout it into the void. We will not listen to it, but it will make you feel better.",
        "[Easter egg removed by moderator]",
        "https://www.youtube.com/watch?v=dQw4w9WgXcQ"
    };

    public static string GetConditionalEasterEgg()
    {
        switch (MetaFile.Data.TimesLaunched)
        {
            // Times Launched
            case 69:
                return "Nice. You started HTCCL 69 times. You are officially a cool person.";
            case 100:
                return "Congratulations! You started HTCCL 100 times! Go outside and touch grass now.";
            case 420:
                return "You started HTCCL 420 times. It's time to blaze it.";
            case 1000:
                return "Wow! You started HTCCL 1000 times! You must really like this mod.";
            case 6969:
                return "Nicenice. You started HTCCL 6969 times. That's very nice.";
            case 10000:
                return "You started HTCCL 10000 times! You are officially a HTCCL addict. Please seek help.";
            case 42069:
                return "You started HTCCL 42069 times. That's a number that's funny for some reason.";
            case 69420:
                return "You started HTCCL 69420 times. That's also a number that's funny for some reason.";
            case 100000:
                return "OMG! You started HTCCL 100000 times! You are officially the biggest HTCCL fan ever!.";
            case 696969:
                return "Nicenicenice. You started HTCCL 696969 times. That's extremely nice.";
            case 1000000:
                return
                    "You started HTCCL one million times! That's it, I'm calling the police. You are a danger to society. Or perhaps you cheated by editing the meta file..";
            case 1000000000:
                return
                    "So, you started HTCCL one billion times. That's cool, I guess. But did you know Ingo doesn't care? That's right! Ingo doesn't care about you or your stupid billion launches. He only cares about dog treats. So stop wasting your time and go buy Ingo some dog treats.";
            case 2147483647:
                return
                    "Oh no! You started HTCCL so many times that the number of times you started it can't go any higher! Congratulations, you broke the game. You must now uninstall HTCCL and never play it again.";
            case < 0:
                return
                    "You started HTCCL a negative number of times. That's not possible. How did you do that? Did you hack the game? Or did you just edit the meta file?";
            case 0:
                return
                    "You started HTCCL zero times. That's not possible. How did you do that? Did you hack the game? Or did you just edit the meta file?";
        }

        // Special Dates
        if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
        {
            return
                "IngoH just announced that HTCCL is in fact going to be deleted. Haha, April Fools!";
        }

        if (DateTime.Now.Month == 12 && DateTime.Now.Day >= 24 && DateTime.Now.Day <= 26)
        {
            if (DateTime.Now.Day == 24)
            {
                if (DateTime.Now.Hour >= 18)
                {
                    return
                        "Merry Christmas Eve! Go spend some time with your family instead of playing this mod.";
                }
                return
                    "Merry Christmas Eve (well, technically it's not quite Christmas Eve yet, but close enough)! Go spend some time with your family instead of playing this mod.";
            }
            return "Merry Christmas! Go spend some time with your family instead of playing this mod.";
        }

        if (DateTime.Now.Month == 10 && DateTime.Now.Day == 31)
        {
            return
                "Boo! Did I scare you? No? Well, I tried. Happy Halloween from IngoH and the rest of the HTCCL team!";
        }

        if (DateTime.Now.Month == 1 && DateTime.Now.Day == 1)
        {
            return
                "Happy New Year! Go make some New Year's resolutions. And don't forget to include \"play HTCCL more\" in them.";
        }

        if (DateTime.Now.Month == 2 && DateTime.Now.Day == 14)
        {
            return
                "Happy Valentine's Day! I hope you have a great day with your loved ones. Or if you don't have any loved ones, I hope you have a great day playing HTCCL.";
        }

        if (DateTime.Now.Month == 2 && DateTime.Now.Day == 29)
        {
            return "Oh no! It's a leap year! That means you have to play HTCCL one more day this year! I'm so sorry.";
        }

        if (DateTime.Now.Month == 2 && DateTime.Now.Day == 30)
        {
            return
                $"Happy February 30th! Wait, what? February 30th isn't a real day? How did you do that? Did you hack {OS.GetOSString("your operating system")}? I'm so confused.";
        }

        if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
        {
            return
                "It's midnight! You should probably go to sleep now. Or you could stay up all night playing HTCCL. I'm not your mom, I can't tell you what to do.";
        }

        if (DateTime.Now.Hour == 3 && DateTime.Now.Minute == 0)
        {
            return "Playing HTCCL at 3 AM challenge? That's so 2016. But hey! I'm not judging. You do you.";
        }

        if (DateTime.Now.Hour == 4 && DateTime.Now.Minute == 20)
        {
            return
                "It's 4:20! You know what that means! It's time to blaze it! Or you could just play HTCCL. That's cool too.";
        }

        if (DateTime.Now.Hour == 25)
        {
            return
                "It's 25 o'clock! That's right, 25 o'clock! Apparently, you broke the space-time continuum. I'm not sure how you did that, but you did.";
        }

        if (DateTime.Now.Day == DateTime.Now.Month && DateTime.Now.Day == DateTime.Now.Hour &&
            DateTime.Now.Day == DateTime.Now.Minute && DateTime.Now.Day == DateTime.Now.Second)
        {
            return "Wow! It's the " + DateTime.Now.Day + " of the " + DateTime.Now.Month + " at " + DateTime.Now.Hour +
                   ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "! How cool is that?";
        }

        if (DateTime.Now.Day == DateTime.Now.Month && DateTime.Now.Day == DateTime.Now.Hour &&
            DateTime.Now.Day == DateTime.Now.Minute)
        {
            return "Wow! It's the " + DateTime.Now.Day + " of the " + DateTime.Now.Month + " at " + DateTime.Now.Hour +
                   ":" + DateTime.Now.Minute + "! That's pretty cool.";
        }

        // System Quirks
        if (SystemInfo.batteryStatus != BatteryStatus.Unknown && SystemInfo.batteryLevel == 0f)
        {
            return "Oh no, your battery level is at 0%! What are you waiting for? Plug it in right now!";
        }
        
        if (SystemInfo.batteryStatus != BatteryStatus.Unknown && SystemInfo.batteryLevel < 0.10 && SystemInfo.batteryLevel > 0f)
        {
            return
                "Psst, hey! I just wanted to let you know that your battery is running low. You should probably plug in your computer.";
        }

        if (SystemInfo.usesReversedZBuffer && Random.Range(0, 1000) == 0)
        {
            return
                "Psst, hey! I just wanted to let you know that you are using a reversed Z-buffer. I don't know what that means, but it sounds bad.";
        }

        if (SystemInfo.systemMemorySize < 1000 && Random.Range(0, 1000) == 0)
        {
            return
                "Psst, hey! I just wanted to let you know that you have less than 1 GB of RAM. I'm not sure how you are even running this game, but you should probably upgrade your computer.";
        }

        if (Environment.UserName.ToLower().StartsWith("ingo") && MetaFile.Data.PreviousUser != Environment.UserName)
        {
            MetaFile.Data.PreviousUser = Environment.UserName;
            return
                "Hey, Ingo! How's it going? I hope you are having a great day. I just wanted to let you know that you are awesome. Keep up the good work!";
        }

        if ((Environment.UserName.ToLower().StartsWith("mat dickie") ||
             Environment.UserName.ToLower().StartsWith("matdickie") ||
             Environment.UserName.ToLower().StartsWith("mdickie")) &&
            MetaFile.Data.PreviousUser != Environment.UserName)
        {
            MetaFile.Data.PreviousUser = Environment.UserName;
            return "Hey, Mat! Thank you for using HTCCL. Have a great day!";
        }

        if (Environment.HasShutdownStarted)
        {
            return
                "It appears that your computer is shutting down at this very moment. Why are you shutting down your computer? Are you done playing HTCCL? I hope you had fun!";
        }

        if (Environment.TickCount <= 60000)
        {
            return "You started HTCCL less than a minute after you started your computer. That's pretty impressive.";
        }

        if (Environment.TickCount >= 1000 * 60 * 60 * 24 * 7 && Random.Range(0, 1000) == 0)
        {
            return
                "You started HTCCL more than a week after you started your computer. I'm not judging, but you should probably restart your computer every once in a while.";
        }

        if (Environment.TickCount < 0 && Random.Range(0, 1000) == 0)
        {
            return
                "Psst, hey! I just wanted to let you know that your computer has been running for more than 24 days. You should probably restart it.";
        }

        if (Plugin.MaxBackups.Value == 69 && Random.Range(0, 1000) == 0)
        {
            return "You set the maximum number of backups to 69. Nice.";
        }

        if (int.Parse(Plugin.PluginVer.Split('.')[2]) >= 10 && Random.Range(0, 1000) == 0)
        {
            return
                $"This is already patch #{Plugin.PluginVer.Split('.')[2]}. of {Plugin.PluginVer.Split('.')[0]}.{Plugin.PluginVer.Split('.')[1]}! That's because Ingo is a good developer who always writes good code that totally works!";
        }

        return null;
    }

    public static string GetEasterEgg()
    {
        string conditionalEasterEgg = GetConditionalEasterEgg();
        if (conditionalEasterEgg != null)
        {
            return conditionalEasterEgg;
        }

        if (Random.Range(0, 100) == 0)
        {
            return EasterEggs[Random.Range(0, EasterEggs.Length)];
        }

        return null;
    }
}