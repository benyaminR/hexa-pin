# if UNITY_EDITOR


using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering;
using UnityEditor;
using UnityEditor.PackageManager;
using System;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;


//add a shortcut to open the ProjectSettings scriptable object
[InitializeOnLoad]
public class ProjectSettingsShortcut
{
    // add a shortcut to open the ProjectSettings scriptable object
    [MenuItem("ProjectSettings/ProjectSettings %&q")]
    public static void OpenProjectSettings()
    {
        Selection.activeObject = Resources.Load("ProjectSettings") as ProjectSettings;
    }
}
// make a scriptable object
[CreateAssetMenu(menuName = "ScriptableObjects/ProjectSettings")]
public class ProjectSettings : ScriptableObject
{

    // add an odin separator and header for other settings
    [Space(10)]
    [Header("General Settings")]
    //current Build Settings
    public BuildSettings buildSettings;

    //change target platform to android or ios
    public TargetPlatform targetPlatform = TargetPlatform.Android;

    //set the company name
    public string companyName = "Fat Moai";

    //set the product name
    public string productName = "Fat Moai Game";

    [ReadOnly] public string bundleIdentifier = "com.FatMoai.FatMoaiGame";

    //set the default icon
    public Texture2D defaultIcon;

    // set orientation
    public UIOrientation screenOrientation = UIOrientation.Portrait;


    // add an odin separator and header for other settings
    [Space(10)]
    [Header("Other Settings")]
    // set color space
    public ColorSpace colorSpace = ColorSpace.Linear;
    // set build path for player settings. User can select a path in the editor
    public string buildPath = "builds";



    // set graphics api
    public GraphicsDeviceType[] graphicsAPIs = new GraphicsDeviceType[] { GraphicsDeviceType.OpenGLES3, GraphicsDeviceType.Vulkan };


    [Space(10)]
    [Header("Publishing Settings")]

    // set keystore alias
    public string keystoreAlias = "FatMoai";
    // set keystore password
    public string keystorePassword = "FatMoai2023";



    [Space(10)]
    [Header("Packages")]

    public ProjectPackage[] packages = new ProjectPackage[]
    {
        new ProjectPackage(){name = "Recorder", package = "com.unity.recorder", use = true},
        new ProjectPackage(){name = "Cinemachine", package = "com.unity.cinemachine", use = false},
        new ProjectPackage(){name = "Android Logcat", package = "com.unity.mobile.android-logcat", use = true},
    };

    public void OnValidate()
    {
        bundleIdentifier = ("com." + companyName + "." + productName).Replace(" ", "");
    }

    static AddAndRemoveRequest Request;

    [Button]
    private void ApplyPackageSettings()
    {
        var removePackages = new List<string>();
        var addPackages = new List<string>();

        foreach (var package in packages)
        {
            if (package.use)
            {
                addPackages.Add(package.package);
            }
            else
            {
                removePackages.Add(package.package);
            }
        }

        Request = Client.AddAndRemove(addPackages.ToArray(), removePackages.ToArray());
        EditorApplication.update += Progress;

    }

    static void Progress()
    {
        if (Request.IsCompleted)
        {
            if (Request.Status == StatusCode.Success)
                Debug.Log("Packages successfully added and removed");
            else if (Request.Status >= StatusCode.Failure)
                Debug.Log(Request.Error.message);

            EditorApplication.update -= Progress;
        }
    }


    private void ApplyPackage(bool add, string package)
    {
        if (add)
        {
            Client.Add(package);
        }
        else
        {
            Client.Remove(package);
        }
    }


    [Button]
    public void ApplySettings()
    {

        //------------------OS Independent Settings------------------//

        //set the company name
        PlayerSettings.companyName = companyName;

        // set product name
        PlayerSettings.productName = productName;


        // set bundle identifier
        PlayerSettings.SetApplicationIdentifier(targetPlatform == TargetPlatform.Android ? BuildTargetGroup.Android : BuildTargetGroup.Unknown, bundleIdentifier);

        // set icon
        PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, new Texture2D[] { defaultIcon });

        // set orientation
        PlayerSettings.defaultInterfaceOrientation = screenOrientation;
        // Other Settings
        // set color space
        PlayerSettings.colorSpace = colorSpace;

        // ------------------OS Dependent Settings------------------//

        if (targetPlatform == TargetPlatform.Android)
        {
            // switch Platform
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

            // set graphics api
            if (targetPlatform == TargetPlatform.Android)
                PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, graphicsAPIs);

            // Production Settings
            if (buildSettings == BuildSettings.Production)
            {
                // set scripting backend
                PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
                // Keystore Settings
                // create a custom keystore
                PlayerSettings.Android.useCustomKeystore = true;

                // set the keystore path
                PlayerSettings.Android.keystoreName = Application.dataPath + "/keystore.keystore";

                // set the keystore password
                PlayerSettings.Android.keystorePass = keystorePassword;

                // set the key alias
                PlayerSettings.Android.keyaliasName = keystoreAlias;

                // set the key password
                PlayerSettings.Android.keyaliasPass = keystorePassword;
            }
            else if (buildSettings == BuildSettings.Debug)
            {
                //Debug Settings
                // set scripting backend
                PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
                // create a custom keystore
                PlayerSettings.Android.useCustomKeystore = false;
            }

        }
        else if (targetPlatform == TargetPlatform.iOS)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
        }

    }


    //show the current build version Code in the inspector as info with odin
    [ShowInInspector, DisplayAsString, LabelText("Build Version Code")]
    public int BuildVersionCode
    {
        get
        {
            return PlayerSettings.Android.bundleVersionCode;
        }
    }

    // show the current build version in the inspector as info with odin
    [ShowInInspector, DisplayAsString, LabelText("Build Version")]
    public string BuildVersion
    {
        get
        {
            return PlayerSettings.bundleVersion;
        }
    }


    [Button]
    public void IncrementBuild()
    {
        // Increment the build version code
        PlayerSettings.Android.bundleVersionCode++;
        // Increment the build version by 0.1 (1.0.0 -> 1.0.1) after reaching 1.0.9 it will be 1.1.0
        PlayerSettings.bundleVersion = PlayerSettings.bundleVersion.Substring(0, PlayerSettings.bundleVersion.LastIndexOf('.') + 1) + (int.Parse(PlayerSettings.bundleVersion.Substring(PlayerSettings.bundleVersion.LastIndexOf('.') + 1)) + 1).ToString();
    }

    public enum TargetPlatform
    {
        Android,
        iOS,
    }

    public enum BuildSettings
    {
        Debug,
        Production,
    }

    [Serializable]
    public class ProjectPackage
    {
        [ReadOnly] public string name;
        [ReadOnly] public string package;
        public bool use;
    }

}
# endif
