using UnityEngine;

public class PlayerToDungeon : MonoBehaviour
{
    public Vector2Int DungeonPos = new();
    public DungeonPlayer DungeonPlayer;
    public bool isMove;


    void OnTriggerEnter(Collider other)
    {
        if (!isMove)
            if (other.gameObject.CompareTag("Stool"))
            {
                if (other.gameObject.GetComponent<Stool>().RoomPos != DungeonPos)
                {
                    DungeonPos = other.gameObject.GetComponent<Stool>().RoomPos;
                    DungeonPlayer.ChangeRoom(DungeonPos);
                    isMove = true;
                }
            }
    }

    void OnTriggerExit(Collider other)
    {
        if (isMove)
            if (other.gameObject.CompareTag("Stool"))
            {
                if (other.gameObject.GetComponent<Stool>().RoomPos == DungeonPos)
                {
                    isMove = false;
                    if (!DungeonPlayer.IsClearRoom(DungeonPos))
                    {
                        DungeonPlayer.CloseDoor(DungeonPos);
                        DungeonPlayer.SpawnObject(DungeonPos);
                    }

                }
            }
    }
}
