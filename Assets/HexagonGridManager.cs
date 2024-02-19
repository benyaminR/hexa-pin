using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HexagonGridManager : SingletonMonoBehaviour<HexagonGridManager>
{
    [SerializeField] private List<GameObject> hexagons;


    private void Awake()
    {
        hexagons = GameObject.FindGameObjectsWithTag("Hexagon").ToList();
    }

    public void UpdateGridState()
    {
        var hexagonsTemp = hexagons.ToArray();
        foreach (var hexagon in hexagonsTemp)
        {
            if (CanHexagonExit(hexagon.transform))
            {
                ExitGrid(hexagon.transform);
            }
        }
    }

    private void ExitGrid(Transform hexagon)
    {
        hexagons.Remove(hexagon.gameObject);
        hexagon.GetComponent<Rigidbody>().isKinematic = false;
    }

    private bool CanHexagonExit(Transform hexagon)
    {
        return !Physics.Raycast(hexagon.position, Vector3.up, 10);
    }

    public bool CheckIfAnyHexagonsAreLeft()
    {
        return hexagons.Count > 0;
    }
}
