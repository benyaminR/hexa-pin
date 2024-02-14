using System.IO;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinManager : SingletonMonoBehaviour<CoinManager>
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinTarget;
    [SerializeField] private float coinMoveSpeed = 2;
    [SerializeField] private float coinLeaveDelay = 0.5f;
    [SerializeField] private Ease ease = Ease.InOutSine;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Camera uiCamera;

    private int coinCount = 0;

    // make singleton

    void Awake()
    {
        base.Awake();
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        UpdateCoinCount();

    }

    public void SpawnCoin(Vector3 position, int count = 2)
    {
        if (uiCamera == null)
            uiCamera = FindAnyObjectByType<Camera>();


        Vector3 worldPos = GetWorldPositionOfUI(coinTarget as RectTransform, uiCamera);
        var path = new Vector3[3];
        path[0] = position;
        path[1] = Vector3.Lerp(position, worldPos, 0.2f) + Vector3.right * Random.Range(0f, 4f);
        path[2] = worldPos;

        for (int i = 0; i < count; i++)
        {

            // create simple bezier path for coin from position to coinTarget
            var rotation = Quaternion.Euler(-90, 0, 0);
            var coin = Instantiate(coinPrefab, position, rotation).transform;
            //worldPos.y = 1f;
            var duration = Random.Range(coinMoveSpeed - 0.5f, coinMoveSpeed + 0.5f);
            coin.DOScale(0f, duration + 0.2f).SetDelay(coinLeaveDelay).SetEase(ease);
            coin.DORotate(Vector3.one * 360, duration, RotateMode.FastBeyond360).SetDelay(coinLeaveDelay).SetEase(ease);
            coin.transform.DOPath(path, duration, PathType.CatmullRom).SetDelay(coinLeaveDelay).SetEase(ease).OnComplete(() =>
                {
                    // increase coin text by 1
                    coinCount++;
                    UpdateCoinCount();
                    coin.gameObject.SetActive(false);
                }
           );
        }
    }

    private void UpdateCoinCount()
    {
        coinText.text = coinCount.ToString();
        PlayerPrefs.SetInt("CoinCount", coinCount);
    }

    Vector3 GetWorldPositionOfUI(RectTransform rectTransform, Camera uiCamera)
    {
        Vector3 worldPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, new Vector2(rectTransform.position.x, rectTransform.position.y), uiCamera, out worldPos);
        return worldPos;
    }
}
