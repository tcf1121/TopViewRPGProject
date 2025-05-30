using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class Room : MonoBehaviour
{
    [SerializeField] public RoomType RoomType;
    [SerializeField] public Material[] RoomMaterial;
    [SerializeField] public Vector2Int RoomPos;
    [SerializeField] public GameObject[] DoorWall;
    [SerializeField] public GameObject[] NoDoorWall;
    [SerializeField] private GameObject[] _doors;
    [SerializeField] private Stool[] _stool;

    public bool isClear;
    UnityAction<bool> IsClear;

    private void OnEnable()
    {
        IsClear += OpenDoor;
    }

    private void OnDisable()
    {
        IsClear -= OpenDoor;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    public Room(RoomType roomType, Vector2Int roomPos)
    {
        RoomType = roomType;
        RoomPos = roomPos;
    }

    public void SetStool(Vector2Int roomPos)
    {
        foreach (Stool stool in _stool)
        {
            stool.SetStool(roomPos);
        }
    }

    public void LinkedRoom(Room[,] rooms)
    {
        if (RoomPos.y - 1 > 0)
            if (rooms[RoomPos.x, RoomPos.y - 1] == null)
            {
                NoDoorWall[0].SetActive(true);
                DoorWall[0].SetActive(false);
            }

        if (RoomPos.y + 1 < 20)
            if (rooms[RoomPos.x, RoomPos.y + 1] == null)
            {
                NoDoorWall[1].SetActive(true);
                DoorWall[1].SetActive(false);
            }

        if (RoomPos.x - 1 > 0)
            if (rooms[RoomPos.x - 1, RoomPos.y] == null)
            {
                NoDoorWall[2].SetActive(true);
                DoorWall[2].SetActive(false);
            }
        if (RoomPos.x + 1 < 20)
            if (rooms[RoomPos.x + 1, RoomPos.y] == null)
            {
                NoDoorWall[3].SetActive(true);
                DoorWall[3].SetActive(false);
            }
    }

    public void OpenDoor(bool Clear)
    {
        if (Clear)
        {
            foreach (GameObject wall in DoorWall)
            {
                if (wall.activeSelf)
                    _doors[Array.IndexOf(DoorWall, wall)].SetActive(false);
            }
        }
        else
        {
            foreach (GameObject wall in DoorWall)
            {
                if (wall.activeSelf)
                    _doors[Array.IndexOf(DoorWall, wall)].SetActive(true);
            }
        }
    }

    public void Clear()
    {
        isClear = true;
        IsClear.Invoke(isClear);
    }

}

public enum RoomType
{
    Normal, // 기본 방
    Portal, // 포탈 방
    Boss,   // 보스 방
    Pitfall,// 함정 방
    Treasure// 보물 방
}