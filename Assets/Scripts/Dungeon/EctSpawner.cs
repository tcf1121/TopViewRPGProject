using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EctSpawner : MonoBehaviour
{
    [SerializeField] GameObject _potalPrefab;
    [SerializeField] GameObject _tresureChestPrefab;
    private int _floor;

    public void SetFloor(int floor)
    {
        _floor = floor;
    }

    public void SpawnPotal(Vector2Int roompos)
    {
        int xPos = (roompos.y - 10) * 10;
        int yPos = (roompos.x - 10) * 16;
        RoomManager.SetMonster(1);
        RoomManager.DieMonster();
        GameObject potal = Instantiate(_potalPrefab);
        potal.transform.position = new Vector3(xPos, 0, yPos);
        potal.GetComponent<Portal>().SetFloor(_floor);
    }

    public void SpawnTresureChest(Vector2Int roompos)
    {
        int xPos = (roompos.y - 10) * 10;
        int yPos = (roompos.x - 10) * 16;
        RoomManager.SetMonster(1);
        GameObject tresureChest = Instantiate(_tresureChestPrefab);
        tresureChest.transform.position = new Vector3(xPos, 0, yPos);
        tresureChest.GetComponent<TreasureChest>().SetFloor(_floor);
    }
}
