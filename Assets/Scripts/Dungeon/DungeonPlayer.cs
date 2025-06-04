using Cinemachine;
using UnityEngine;

public class DungeonPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private PlayerToDungeon _playerToDungeon;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private GameObject _msPrefab;
    private MonsterSpawner _monsterSpawner;
    private EctSpawner _ectSpawner;
    private Room[,] _roomArray = new Room[20, 20];


    public void Init()
    {
        _player = GameObject.Find("Player");
        _monsterSpawner = _msPrefab.GetComponent<MonsterSpawner>();
        _ectSpawner = GetComponent<EctSpawner>();
        _playerToDungeon = _player.AddComponent<PlayerToDungeon>();
        _playerToDungeon.DungeonPlayer = this;
        RoomManager.isClear += ClearRoom;
    }

    public void SetFloor(int floor)
    {
        _player.gameObject.transform.position = new Vector3(0, 0, 0);
        _playerToDungeon.DungeonPos = new Vector2Int(10, 10);
        CloseDoor(_playerToDungeon.DungeonPos);
        ChangeRoom(_playerToDungeon.DungeonPos);
        _monsterSpawner.SetFloor(floor);
        _ectSpawner.SetFloor(floor);
        SpawnObject(_playerToDungeon.DungeonPos);

    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Z))
    //     {
    //         ClearRoom(_playerToDungeon.DungeonPos);
    //     }
    // }

    public void SetRoomArray(Room[,] roomArray)
    {
        _roomArray = roomArray;
    }

    public void ChangeRoom(Vector2Int pos)
    {
        _camera.Follow = _roomArray[pos.x, pos.y].gameObject.transform;
        RoomManager.SetRoom(pos);
    }

    public void CloseDoor(Vector2Int pos)
    {
        _roomArray[pos.x, pos.y].OpenDoor(false);
    }

    public bool IsClearRoom(Vector2Int pos)
    {
        return _roomArray[pos.x, pos.y].isClear;
    }

    public void SpawnObject(Vector2Int pos)
    {
        if (_roomArray[pos.x, pos.y].RoomType == RoomType.Normal)
            _monsterSpawner.SpawnNormalMonster(pos);
        if (_roomArray[pos.x, pos.y].RoomType == RoomType.Pitfall)
            _monsterSpawner.SpawnNormalMonster(pos, true);
        if (_roomArray[pos.x, pos.y].RoomType == RoomType.Boss)
            _monsterSpawner.SpawnBossMonster(pos);
        if (_roomArray[pos.x, pos.y].RoomType == RoomType.Portal)
            _ectSpawner.SpawnPotal(pos);
        if (_roomArray[pos.x, pos.y].RoomType == RoomType.Treasure)
            _ectSpawner.SpawnTresureChest(pos);
    }

    public void ClearRoom(Vector2Int pos)
    {
        _roomArray[pos.x, pos.y].Clear();
    }
}
