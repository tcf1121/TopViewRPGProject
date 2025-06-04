using UnityEngine;

public class Stool : MonoBehaviour
{
    public Vector2Int RoomPos = new();

    public void SetStool(Vector2Int roomPos)
    {
        RoomPos = roomPos;
    }
}
