# if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.IO;
using Sirenix.OdinInspector;

public class ColroGenerator : MonoBehaviour
{

    [Button]
    public void GenerateMaterials()
    {
        // Define the path where materials will be saved
        string path = "Assets/PastelMaterials"; // Change this if needed
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string[] colorNames = {
            "PastelPink", "PastelRed", "PastelPurple", "PastelViolet", "PastelBlue",
            "PastelGreen", "PastelYellow", "PastelGray", "PastelMagenta", "PastelOrange",
            "PowderBlue", "BlushPink", "BabyBlue", "Lilac", "MintGreen",
            "MellowYellow", "Melon", "Peach", "Lavender", "Thistle",
            "Periwinkle", "Mauve", "TiffanyBlue", "TeaGreen", "Champagne",
            "FlamingoPink", "Tangerine", "Buttermilk", "SalmonPink", "MagicMint",
            "LemonChiffon", "TurquoiseBlue", "SteelBlue1", "Plum", "Orchid",
            "Goldenrod1", "PaleTurquoise", "SkyBlue1", "SandyBrown", "RosyBrown1",
            "RoyalBlue1", "MediumPurple1", "MediumOrchid1", "Maroon1", "Magenta2",
            "LightSteelBlue", "LightSkyBlue1", "LightSalmon1", "LightPink1", "LightGoldenrod1"
        };

        Color[] colors = {
            new Color(255/255f, 209/255f, 220/255f), new Color(255/255f, 105/255f, 97/255f),
            new Color(219/255f, 112/255f, 219/255f), new Color(199/255f, 159/255f, 239/255f),
            new Color(173/255f, 216/255f, 230/255f), new Color(119/255f, 221/255f, 119/255f),
            new Color(253/255f, 253/255f, 150/255f), new Color(205/255f, 205/255f, 205/255f),
            new Color(255/255f, 179/255f, 255/255f), new Color(255/255f, 179/255f, 71/255f),
            new Color(176/255f, 224/255f, 230/255f), new Color(255/255f, 111/255f, 105/255f),
            new Color(189/255f, 201/255f, 255/255f), new Color(200/255f, 162/255f, 200/255f),
            new Color(170/255f, 240/255f, 209/255f), new Color(248/255f, 226/255f, 142/255f),
            new Color(253/255f, 188/255f, 180/255f), new Color(255/255f, 229/255f, 180/255f),
            new Color(230/255f, 230/255f, 250/255f), new Color(216/255f, 191/255f, 216/255f),
            new Color(204/255f, 204/255f, 255/255f), new Color(224/255f, 176/255f, 255/255f),
            new Color(10/255f, 186/255f, 181/255f), new Color(208/255f, 240/255f, 192/255f),
            new Color(250/255f, 230/255f, 250/255f), new Color(252/255f, 142/255f, 172/255f),
            new Color(255/255f, 170/255f, 79/255f), new Color(254/255f, 241/255f, 181/255f),
            new Color(255/255f, 145/255f, 164/255f), new Color(170/255f, 240/255f, 209/255f),
            new Color(255/255f, 250/255f, 205/255f), new Color(64/255f, 224/255f, 208/255f),
            new Color(99/255f, 184/255f, 255/255f), new Color(221/255f, 160/255f, 221/255f),
            new Color(218/255f, 112/255f, 214/255f), new Color(255/255f, 193/255f, 37/255f),
            new Color(175/255f, 238/255f, 238/255f), new Color(82/255f, 139/255f, 255/255f),
            new Color(244/255f, 164/255f, 96/255f), new Color(255/255f, 193/255f, 193/255f),
            new Color(65/255f, 105/255f, 225/255f), new Color(147/255f, 112/255f, 219/255f),
            new Color(224/255f, 102/255f, 255/255f), new Color(255/255f, 52/255f, 179/255f),
            new Color(238/255f, 92/255f, 238/255f), new Color(176/255f, 224/255f, 230/255f),
            new Color(82/255f, 139/255f, 255/255f), new Color(255/255f, 160/255f, 122/255f),
            new Color(255/255f, 182/255f, 193/255f), new Color(238/255f, 221/255f, 130/255f)
        };

        for (int i = 0; i < colorNames.Length; i++)
        {
            Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            mat.color = colors[i];
            AssetDatabase.CreateAsset(mat, Path.Combine(path, $"{colorNames[i]}.mat"));
        }

        AssetDatabase.SaveAssets();
    }
}
#endif