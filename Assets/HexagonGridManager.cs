using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HexagonGridManager : SingletonMonoBehaviour<HexagonGridManager>
{
    [SerializeField] private List<Transform> hexagons;


    public void UpdateGridState()
    {
        var hexagonsTemp = hexagons.ToArray();
        foreach (var hexagon in hexagonsTemp)
        {
            if (CanHexagonExit(hexagon))
            {
                ExitGrid(hexagon);
            }
        }
    }

    private void ExitGrid(Transform hexagon)
    {
        hexagons.Remove(hexagon);
        hexagon.AddComponent<Rigidbody>();
    }

    private bool CanHexagonExit(Transform hexagon)
    {
        return !Physics.Raycast(hexagon.position, Vector3.up, 10);
    }
}
