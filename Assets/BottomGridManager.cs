using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class BottomGridManager : SingletonMonoBehaviour<BottomGridManager>
{
    [SerializeField] private GridSlot[] gridSlots = new GridSlot[7];


    void Update()
    {
        for (int i = 0; i < gridSlots.Length; i++)
        {
            var gridSlot = gridSlots[i];
            if (gridSlot.hexagon != null)
            {
                gridSlot.hexagon.transform.position = Vector3.Lerp(gridSlot.hexagon.transform.position, gridSlot.slot.transform.position, 5f * Time.deltaTime);
            }
        }
    }

    public void AddHexagonToMatchGrid(Transform hexagon)
    {

        if (IsHexagonAlreadyAdded(hexagon)) return;

        var color = hexagon.GetComponent<MaterialOverrider>().materials[0].color;

        var index = GetEmptySlotIndex(color);
        if (index == -1)
        {
            return;
        }
        else
        {

            var rg = hexagon.GetComponent<Rigidbody>();
            rg.useGravity = false;
            rg.isKinematic = true;
            hexagon.transform.DOScale(0.5f, 0.5f);
            gridSlots[index].hexagon = hexagon;
            gridSlots[index].hexagonColor = color;
            Invoke(nameof(UpdateGrid), 0.5f);
        }

    }

    private bool IsHexagonAlreadyAdded(Transform hexagon)
    {
        for (int i = 0; i < gridSlots.Length; i++)
        {
            var gridSlot = gridSlots[i];
            if (gridSlot.hexagon == hexagon)
            {
                return true;
            }
        }
        return false;
    }

    // returns the index of the empty slot after shifting
    private int GetEmptySlotIndex(Color color)
    {
        for (int i = 0; i < gridSlots.Length; i++)
        {
            var gridSlot = gridSlots[i];

            // check if there is any empty slots

            if (gridSlot.hexagon == null)
            {

                // Find all the groups of cars of the same color in the grid
                var group = new List<GridSlot>();
                for (int j = 0; j < gridSlots.Length; j++)
                {
                    var slot = gridSlots[j];
                    if (slot.hexagon != null && slot.hexagonColor == color)
                    {
                        group.Add(slot);
                    }
                }

                // if there are no groups, return the first empty slot
                if (group.Count == 0)
                {
                    return i;
                }

                // return the next slot after the last slot in the biggest group
                var emptyIndex = group[group.Count - 1].index + 1;

                // shift the grid from emptyIndex to the end of the grid by one
                for (int j = gridSlots.Length - 1; j > emptyIndex; j--)
                {
                    gridSlots[j].hexagon = gridSlots[j - 1].hexagon;
                }
                // empty the grid
                gridSlots[emptyIndex].hexagon = null;
                return emptyIndex;
            }
        }
        //Debug.LogError("No empty slots found!");
        return -1;
    }
    public void UpdateGrid()
    {
        // find all the groups of hexagons of the same color in the grid
        var group = new List<GridSlot>();
        for (int i = 0; i < gridSlots.Length; i++)
        {
            var slot = gridSlots[i];
            if (slot.hexagon != null)
            {
                group.Add(slot);
                for (int j = i + 1; j < gridSlots.Length; j++)
                {
                    var nextSlot = gridSlots[j];
                    if (nextSlot.hexagon != null && nextSlot.hexagonColor == slot.hexagonColor)
                    {
                        group.Add(nextSlot);
                    }
                    else
                    {
                        break;
                    }
                }

                if (group.Count == 3)
                {
                    break;
                }
                else
                {
                    group.Clear();
                }
            }
        }

        // remove groups of hexagons of the same color that have at least 3 hexagons

        if (group.Count == 3)
        {
            MMVibrationManager.Haptic(HapticTypes.Success);
            for (int i = 0; i < group.Count; i++)
            {
                GridSlot slot = group[i];
                var temphexagon = slot.hexagon;

                temphexagon.transform.DOMove(temphexagon.transform.forward * 10, 0.35f + i * 0.15f).SetRelative(true).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    temphexagon.gameObject.SetActive(false);
                    //GameManager.Instance.Removehexagon(temphexagon);
                }).OnStart(() =>
                {
                    //CoinManager.Instance.SpawnCoin(temphexagon.transform.position, 1);
                });
                slot.hexagon = null;
            }


            var emptyIndex = group[0].index;
            for (int i = emptyIndex; i < gridSlots.Length - 3; i++)
            {
                gridSlots[i].hexagon = gridSlots[i + 3].hexagon;
            }

            for (int i = gridSlots.Length - 3; i < gridSlots.Length; i++)
            {
                gridSlots[i].hexagon = null;
            }
        }



        if (IsGridFilled())
        {
            Debug.Log("Grid is filled");
            return;
        }

    }

    private bool IsGridFilled()
    {
        for (int i = 0; i < gridSlots.Length; i++)
        {
            if (gridSlots[i].hexagon == null)
            {
                return false;
            }
        }

        return true;
    }


}

[Serializable]
public class GridSlot
{
    public int index;
    public GameObject slot;
    public Transform hexagon;
    public Color hexagonColor;
}
