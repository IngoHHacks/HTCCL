namespace HTCCL.Patches;

[HarmonyPatch]
internal class StackTracePatch
{

    public static bool HasException = false;
    public static string ExceptionMessage = "";
    public static string ExceptionStackTrace = "";
    public static string ExceptionScreen = "";

    [HarmonyPatch(typeof(StackTraceUtility), nameof(ExtractStringFromExceptionInternal))]
    [HarmonyPostfix]
    public static void ExtractStringFromExceptionInternal(string message, string stackTrace)
    {
        HasException = true;
        ExceptionMessage = message;
        ExceptionStackTrace = stackTrace;
        ExceptionScreen = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }
}