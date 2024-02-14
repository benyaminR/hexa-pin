using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> levels;
    [SerializeField] private List<GameObject> themes;

    // load all levels from resoures
    [Button]
    void LoadAllLevelsFromResources()
    {
        levels = new List<GameObject>();
        var levelObjects = Resources.LoadAll<GameObject>("Levels");
        System.Array.Sort(levelObjects,
    (x, y) => int.Parse(x.name.Substring(6)).CompareTo(int.Parse(y.name.Substring(6))));

        foreach (var levelObject in levelObjects)
        {
            levels.Add(levelObject as GameObject);
        }
    }

    void Awake()
    {
        // spawn level based on the level index
        int level = PlayerPrefs.GetInt("CurrentLevel", 0);
        Instantiate(levels[level % levels.Count]);

        // change theme every 16 level
        int theme = level / 16;
        Instantiate(themes[theme % themes.Count]);

        //SundaySDK.Tracking.TrackLevelStart(level);
    }
}
