using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

/// 
/// Put me inside an Editor folder
/// 
/// Add a Build menu on the toolbar to automate multiple build for different platform
/// 
/// Use #define BUILD in your code if you have build specification 
/// Specify all your Target to build All
/// 
/// Install to Android device using adb install -r "pathofApk"
/// 
public class BuildCommand : MonoBehaviour
{
    const string androidKeystorePass = "";
    const string androidKeyaliasName = "";
    const string androidKeyaliasPass = "";

    static BuildTarget[] targetToBuildAll =
    {
        BuildTarget.Android,
        BuildTarget.WebGL
    };

    public static string ProductName
    {
        get { return PlayerSettings.productName; }
    }

    private static string GetBuildPathRoot(BuildTarget buildTarget, BuildTargetGroup targetGroup)
    {
        DirectoryInfo directory = Directory.GetParent(Application.dataPath);
        string buildDir = Path.Combine(directory.FullName, "Build");
        if (!Directory.Exists(buildDir))
        {
            Directory.CreateDirectory(buildDir);
        }
        
        string buildTargetDir = Path.Combine(buildDir, buildTarget.ToString());
        if (!Directory.Exists(buildTargetDir))
        {
            Directory.CreateDirectory(buildTargetDir);
        }

        System.DateTime now = System.DateTime.Now;
        string dayStr = $"{now:yyyy-MM-dd}";
        return Path.Combine(buildTargetDir, dayStr);
    }

    private static string GetFileName(BuildTarget buildTarget, BuildTargetGroup targetGroup, bool inculeMinuteInBuildName) {
        System.DateTime now = System.DateTime.Now;
        string dayStr = $"{now:yyyy-MM-dd}";
        string hourStr = inculeMinuteInBuildName ? $"{now:HH-mm}" : $"{now:HH-00}"; // zero minute
        string uniqueTime = $"{dayStr.Replace("-", "")}-{hourStr.Replace("-", "")}";
        return $"{ProductName}-{uniqueTime}" + GetExtension(buildTarget);
    }

    static int AndroidLastBuildVersionCode
    {
        get { return PlayerPrefs.GetInt("LastVersionCode", -1); }
        set { PlayerPrefs.SetInt("LastVersionCode", value); }
    }

    static BuildTargetGroup ConvertBuildTarget(BuildTarget buildTarget)
    {
        switch (buildTarget)
        {
            case BuildTarget.StandaloneOSX:
            case BuildTarget.iOS:
                return BuildTargetGroup.iOS;
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
            case BuildTarget.StandaloneLinux64:
                return BuildTargetGroup.Standalone;
            case BuildTarget.Android:
                return BuildTargetGroup.Android;
            case BuildTarget.WebGL:
                return BuildTargetGroup.WebGL;
            case BuildTarget.WSAPlayer:
                return BuildTargetGroup.WSA;
            case BuildTarget.PS4:
                return BuildTargetGroup.PS4;
            case BuildTarget.XboxOne:
                return BuildTargetGroup.XboxOne;
            case BuildTarget.tvOS:
                return BuildTargetGroup.tvOS;
            case BuildTarget.Switch:
                return BuildTargetGroup.Switch;
            default:
                return BuildTargetGroup.Standalone;
        }
    }

    static string GetExtension(BuildTarget buildTarget)
    {
        switch (buildTarget)
        {
            case BuildTarget.StandaloneOSX:
                break;
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return ".exe";
            case BuildTarget.iOS:
                break;
            case BuildTarget.Android:
                return ".apk";
            case BuildTarget.WebGL:
                return "";
            case BuildTarget.WSAPlayer:
                break;
            case BuildTarget.StandaloneLinux64:
                break;
            case BuildTarget.PS4:
                break;
            case BuildTarget.XboxOne:
                break;
            case BuildTarget.tvOS:
                break;
            case BuildTarget.Switch:
                break;
            case BuildTarget.NoTarget:
                break;
            default:
                break;
        }

        return ".unknown";
    }

    static BuildPlayerOptions GetDefaultPlayerOptions()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

        List<string> listScenes = new List<string>();
        foreach (var s in EditorBuildSettings.scenes)
        {
            if (s.enabled)
                listScenes.Add(s.path);
        }

        buildPlayerOptions.scenes = listScenes.ToArray();
        buildPlayerOptions.options = BuildOptions.None;

        return buildPlayerOptions;
    }

    private static Dictionary<string, string> GetCommandLineArgs() {
        Dictionary<string, string> result = new Dictionary<string, string>();
        string[] args = System.Environment.GetCommandLineArgs();
        // result["-unityPath"] = args[0]; // can be ignore as this is Unity path
        for (int i = 1; i < args.Length; i++) {
            string key = args[i];
            if (!key.StartsWith("-")) {
                continue;
            }

            string val = i == args.Length - 1 ? "" : args[i + 1];
            if (val.StartsWith("-")) {
                val = "";
            }

            result[key] = val;
        }

        return result;
    }

    static void DefaultBuild(BuildTarget buildTarget) {
        Dictionary<string, string> args = GetCommandLineArgs();
        bool inculeMinuteInBuildName = false;
        if (args.TryGetValue("inculeMinuteInBuildName", out string inculeMinuteInBuildNameStr)) {
            inculeMinuteInBuildName = bool.Parse(inculeMinuteInBuildNameStr);
        }
        
        foreach (var arg in args) {
            Debug.LogError($"GetCommandLineArgs key {arg.Key} __ value {arg.Value}");
        }
        
        // Debug.LogError(args["fake-key"]);
        BuildTargetGroup targetGroup = ConvertBuildTarget(buildTarget);

        string path = GetBuildPathRoot(buildTarget, targetGroup);
        string name = GetFileName(buildTarget, targetGroup, inculeMinuteInBuildName);


        string defineSymbole = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

        if (buildTarget == BuildTarget.Android)
        {
            PlayerSettings.Android.keystorePass = androidKeystorePass;
            PlayerSettings.Android.keyaliasName = androidKeyaliasName;
            PlayerSettings.Android.keyaliasPass = androidKeyaliasPass;
        }

        BuildPlayerOptions buildPlayerOptions = GetDefaultPlayerOptions();

        buildPlayerOptions.locationPathName = Path.Combine(path, name);
        buildPlayerOptions.target = buildTarget;

        EditorUserBuildSettings.SwitchActiveBuildTarget(targetGroup, buildTarget);

        string result = buildPlayerOptions.locationPathName + ": " + BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log(result);

        if (buildTarget == BuildTarget.Android)
        {
            AndroidLastBuildVersionCode = PlayerSettings.Android.bundleVersionCode;
        }

        EditorUtility.RevealInFinder(path);
    }

    [MenuItem("Build/Build Specific/Build Android")]
    static void BuildAndroid()
    {
        DefaultBuild(BuildTarget.Android);
    }

    [MenuItem("Build/Build Specific/Build WebGl")]
    static void BuildWebGl()
    {
        DefaultBuild(BuildTarget.WebGL);
    }

    [MenuItem("Build/Build Specific/Build Win32")]
    static void BuildWin32()
    {
        DefaultBuild(BuildTarget.StandaloneWindows);
    }

    [MenuItem("Build/Build Specific/Build Win64")]
    static void BuildWin64()
    {
        DefaultBuild(BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Build/Get Build Number")]
    static void BuildNumber()
    {
        Debug.Log("Current/Last: " + PlayerSettings.Android.bundleVersionCode + "/" + AndroidLastBuildVersionCode);
    }

    [MenuItem("Build/Build Number/Up Build Number")]
    static void BuildNumberUp()
    {
        PlayerSettings.Android.bundleVersionCode++;
        BuildNumber();
    }

    [MenuItem("Build/Build Number/Down Build Number")]
    static void BuildNumberDown()
    {
        PlayerSettings.Android.bundleVersionCode--;
        BuildNumber();
    }

    [MenuItem("Build/Build All")]
    static void BuildAll()
    {
        List<BuildTarget> buildTargetLeft = new List<BuildTarget>(targetToBuildAll);

        if (buildTargetLeft.Contains(EditorUserBuildSettings.activeBuildTarget))
        {
            DefaultBuild(EditorUserBuildSettings.activeBuildTarget);
            buildTargetLeft.Remove(EditorUserBuildSettings.activeBuildTarget);
        }

        foreach (var b in buildTargetLeft)
        {
            DefaultBuild(b);
        }
    }
}