using UnityEngine;
using System.IO;

public class FolderStructureGenerator : MonoBehaviour
{
    private static readonly string[] folders = {
        "Art/2D/Sprites",
        "Art/2D/UI",
        "Art/2D/Animations",
        "Art/2D/Materials",
        "Art/3D/Models",
        "Art/3D/Textures",
        "Art/3D/Materials",
        "Art/3D/Animations",
        "Art/3D/Shaders",
        "Audio/Music",
        "Audio/Sound Effects",
        "Scripts/Managers",
        "Scripts/Controllers",
        "Scripts/Gameplay",
        "Scripts/UI",
        "Scripts/Utilities",
        "Scenes",
        "Prefabs/Characters",
        "Prefabs/Objects",
        "Prefabs/UI",
        "Prefabs/Effects",
        "Prefabs/Environment",
        "Prefabs/Collectibles",
        "Prefabs/Enemies",
        "Resources/Configurations",
        "Resources/Localization",
        "Plugins",
        "Documentation",
        "Builds/Android",
        "Builds/iOS",
    };

    private void Start()
    {
        GenerateFolderStructure();
    }

    private void GenerateFolderStructure()
    {
        string projectPath = Application.dataPath;
        foreach (string folder in folders)
        {
            string path = Path.Combine(projectPath, folder);
            Directory.CreateDirectory(path);
        }
        Debug.Log("Folder structure generated successfully!");
    }
}

