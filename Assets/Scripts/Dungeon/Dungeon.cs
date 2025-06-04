using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dungeon : MonoBehaviour
{
    [SerializeField] private Image _timebar;
    [SerializeField] private GameObject _roomPrefab;
    [SerializeField] private DungeonPlayer _dgPlayer;
    [SerializeField] private float _roomWidth;
    [SerializeField] private float _roomheight;
    private Room[,] _roomArray = new Room[20, 20];
    private int _roomNum;
    List<RoomType> _roomTypes;
    private List<GameObject> _dungeonRoomList = new();
    private List<Vector2Int> newRoomList = new();
    private List<Vector2Int> RemovedRoomList = new();

    private float gameTime = 900f;
    private float times = 0;

    private void Awake() => Init();

    public void Init()
    {
        GameManager.Dungeon = this;
        _dgPlayer.Init();
        StartDungeon(1);
    }

    void Update()
    {
        times += Time.deltaTime;
        _timebar.fillAmount = (gameTime - times) / gameTime;
        if (times >= gameTime)
        {
            LoadingSceneManager.LoadScene(2);
        }
    }

    public void StartDungeon(int floor)
    {
        CreateDungeon(floor);
        SetLinkedRoom();
        _dgPlayer.SetRoomArray(_roomArray);
        _dgPlayer.SetFloor(floor);
    }

    public void EndDungeon()
    {
        for (int x = 0; x < 20; x++)
            for (int y = 0; y < 20; y++)
                _roomArray[y, x] = null;
        foreach (GameObject obj in _dungeonRoomList)
            Destroy(obj);
        newRoomList.Clear();
        RemovedRoomList.Clear();
    }

    private void CreateDungeon(int floor)
    {
        _dungeonRoomList.Clear();
        SetDungeon(floor);


        int x = _roomArray.GetLength(0) / 2;
        int y = _roomArray.GetLength(1) / 2;

        Room startRoom = _roomArray[x, y] = CreateRoom(new Vector2Int(x, y), true);

        for (int i = 0; i < _roomNum; i++)
        {
            x = startRoom.RoomPos.x;
            y = startRoom.RoomPos.y;

            SetRoom(new Vector2Int(x + 1, y));
            SetRoom(new Vector2Int(x - 1, y));
            SetRoom(new Vector2Int(x, y + 1));
            SetRoom(new Vector2Int(x, y - 1));

            Vector2Int newRoomPos = newRoomList[Random.Range(0, newRoomList.Count)];
            startRoom = CreateRoom(newRoomPos);
            newRoomList.Remove(newRoomPos);
        }
    }

    private void SetDungeon(int floor)
    {
        _roomNum = floor * 10 - 1;
        _roomTypes = new List<RoomType>();

        for (int i = 0; i < _roomNum; i++)
        {
            if (i == 0)
                _roomTypes.Add(RoomType.Boss);
            else if (i < floor + 1)
                _roomTypes.Add(RoomType.Portal);
            else
            {
                int randRoom = Random.Range(1, 101);
                if (randRoom == 1)
                    _roomTypes.Add(RoomType.Treasure);
                else if (randRoom == 2)
                    _roomTypes.Add(RoomType.Pitfall);
                else
                    _roomTypes.Add(RoomType.Normal);
            }
        }
    }

    private void SetRoom(Vector2Int roomPos)
    {
        if (_roomArray[roomPos.x, roomPos.y] == null)
        {
            if (newRoomList.Contains(roomPos))
            {
                newRoomList.Remove(roomPos);
                RemovedRoomList.Add(roomPos);
            }
            else if (!RemovedRoomList.Contains(roomPos))
            {
                newRoomList.Add(roomPos);
            }
        }
    }

    private Room CreateRoom(Vector2Int roompos, bool start = false)
    {
        GameObject newRoom = Instantiate(_roomPrefab, transform);

        if (start)
        {
            newRoom.GetComponent<Room>().RoomType = RoomType.Normal;
        }
        else
        {
            int rand = Random.Range(0, _roomTypes.Count);
            newRoom.GetComponent<Room>().RoomType = _roomTypes[rand];
            _roomTypes.RemoveAt(rand);
        }
        newRoom.GetComponent<Room>().RoomPos = roompos;
        newRoom.GetComponent<Room>().SetStool(roompos);
        _roomArray[roompos.x, roompos.y] = newRoom.GetComponent<Room>();
        newRoom.transform.position = new Vector3((roompos.y - 10) * _roomheight,
         0, (roompos.x - 10) * _roomWidth);

        _dungeonRoomList.Add(newRoom);
        return _roomArray[roompos.x, roompos.y];
    }

    private void SetLinkedRoom()
    {
        foreach (GameObject room in _dungeonRoomList)
        {
            room.GetComponent<Room>().LinkedRoom(_roomArray);
        }
    }


}

