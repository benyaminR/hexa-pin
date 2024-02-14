using System;
using System.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private Image filler;

    private float _currentProgress;
    private void Start()
    {
        StartLoad();
    }

    [Button]
    public void LoadingScene()
    {
        StartLoad();
    }

    public void StartLoad()
    {
        progress.text = "0%";
        filler.fillAmount = 0;
        _currentProgress = 0;
        gameObject.SetActive(true);
        var scene = SceneManager.LoadSceneAsync(1);
        scene.allowSceneActivation = false;
        DOTween.To(value =>
        {
            filler.fillAmount = value;
            progress.text = Mathf.CeilToInt(value * 100) + "%";
        }, 0f, 1f, 2.5f).SetEase(Ease.OutQuint).OnComplete(() => { scene.allowSceneActivation = true; });
    }
}