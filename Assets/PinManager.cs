using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Lean.Touch;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    [SerializeField] private List<LeanSelectable> pinSelectables;
    [SerializeField] private float removeSpeed = 2.5f;
    [SerializeField] private float removeDistance = 10;
    private void Awake()
    {
        pinSelectables = FindObjectsOfType<LeanSelectable>().ToList();

        foreach (var pinSelectable in pinSelectables)
        {
            pinSelectable.OnSelect.AddListener((leanFinger) => OnPinSelected(leanFinger, pinSelectable));
        }

    }

    private void OnPinSelected(LeanFinger leanFinger, LeanSelectable leanSelectable)
    {
        var pin = leanSelectable.transform;
        MMVibrationManager.Haptic(HapticTypes.Selection);
        pin.DOMove(-pin.forward * removeDistance, removeSpeed).SetSpeedBased(true).SetRelative(true).OnComplete(() =>
        {
            pin.gameObject.SetActive(false);
        }).OnUpdate(() =>
        {
            HexagonGridManager.Instance.UpdateGridState();
        });
    }
}
