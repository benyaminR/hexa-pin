using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine;

public class BottomGridManager : SingletonMonoBehaviour<BottomGridManager>
{
    [SerializeField] private GridSlot[] gridSlots = new GridSlot[7];
    [SerializeField] private Transform[] gridSlotOrigins = new Transform[7];

    //public bool _repositioningActive;
    void Update()
    {
        UpdateGrid();

        //if (!_repositioningActive) return;
        //_repositioningActive = false;
        for (int i = 0; i < gridSlots.Length; i++)
        {
            var gridSlot = gridSlots[i];
            var gridSlotOriginPosition = gridSlotOrigins[i].position;
            if (gridSlot.hexagon != null)
            {
                gridSlot.hexagon.transform.position = Vector3.Lerp(gridSlot.hexagon.transform.position, gridSlotOriginPosition, 5f * Time.deltaTime);

                // // keep the repositioning active if there is at least one element not at right position
                // if (Vector3.Distance(gridSlot.hexagon.position, gridSlotOriginPosition) >= 0.01f)
                // {
                //     gridSlot.state = HexagonState.Moving;
                //     _repositioningActive = true;
                // }
                // else
                // {
                //     gridSlot.state = HexagonState.Positioned;
                // }
            }
        }

        //if (_repositioningActive) return;

    }

    public void AddHexagonToMatchGrid(Transform hexagon)
    {

        //if (IsHexagonAlreadyAdded(hexagon)) return;

        var color = hexagon.GetComponent<MaterialOverrider>().materials[0].color;
        var index = GetEmptySlotIndex(color);
        if (index == -1)
        {
            return;
        }
        else
        {

            Debug.Log("Adding " + hexagon.name);
            var rg = hexagon.GetComponent<Rigidbody>();
            rg.useGravity = false;
            rg.isKinematic = true;
            var collider = hexagon.GetComponent<Collider>();
            collider.enabled = false;
            hexagon.transform.DORotate(new Vector3(90f, 0, 0), 0.5f);
            hexagon.transform.DOScale(0.5f, 0.5f);
            gridSlots[index].hexagon = hexagon;
            gridSlots[index].hexagonColor = color;
            //gridSlots[index].state = HexagonState.Moving;
            //_repositioningActive = true;
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

    // returns the index of the empty slot
    private int GetEmptySlotIndex(Color color)
    {
        for (int i = 0; i < gridSlots.Length; i++)
        {
            var gridSlot = gridSlots[i];

            // check if there is any empty slots

            if (gridSlot.hexagon == null)
            {
                return i;
            }
        }
        //Debug.LogError("No empty slots found!");
        return -1;
    }


    // Is there a case 

    public void UpdateGrid()
    {

        SortGridIfNeeded();

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
                    if (nextSlot.hexagon != null && nextSlot.hexagonColor == slot.hexagonColor /*&& slot.state == HexagonState.Positioned*/)
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
            var firstInGroup = group[0].hexagon.transform.position;
            for (int i = 0; i < group.Count; i++)
            {
                GridSlot slot = group[i];
                EmptySlot(slot);
                var temphexagon = slot.hexagon;
                temphexagon.transform.DOMove(firstInGroup, 0.35f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    temphexagon.gameObject.SetActive(false);
                });
            }

        }

        RemoveGapsAndShift();
        //_repositioningActive = true;

        if (IsGridFilled())
        {
            Debug.Log("Grid is filled");
            return;
        }

    }

    private void EmptySlot(GridSlot slot)
    {
        for (int i = 0; i < gridSlots.Length; i++)
        {
            if (gridSlots[i].hexagon == slot.hexagon)
            {
                Debug.Log("Emptied " + gridSlots[i].hexagon.name);
                gridSlots[i] = new GridSlot();
                return;
            }
        }
    }

    private void RemoveGapsAndShift()
    {
        int currentIndex = 0;

        for (int i = 0; i < gridSlots.Length; i++)
        {
            if (gridSlots[i].hexagon != null)
            {
                gridSlots[currentIndex] = gridSlots[i];
                currentIndex++;
            }
        }

        // Fill the remaining elements with null
        for (int i = currentIndex; i < gridSlots.Length; i++)
        {
            gridSlots[i] = new GridSlot();
        }
    }
    private void SortGridIfNeeded()
    {
        Array.Sort(gridSlots, CompareGridSlotsByColor);
    }
    private int CompareGridSlotsByColor(GridSlot slot1, GridSlot slot2)
    {
        // Compare by hexagonColor
        float color1Value = slot1.hexagonColor.r + slot1.hexagonColor.g + slot1.hexagonColor.b;
        float color2Value = slot2.hexagonColor.r + slot2.hexagonColor.g + slot2.hexagonColor.b;

        return color2Value.CompareTo(color1Value);
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
    public Transform hexagon;
    public Color hexagonColor;
    //public HexagonState state = HexagonState.Moving;
}

public enum HexagonState
{
    Moving,
    Positioned
}
