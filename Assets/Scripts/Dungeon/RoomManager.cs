using UnityEngine;
using UnityEngine.Events;

public static class RoomManager
{
    static public UnityAction<Vector2Int> isClear;
    static private int monsterNum;
    static private Vector2Int roomPos = new();

    static public void SetRoom(Vector2Int pos)
    {
        roomPos = pos;
    }

    static public void SetMonster(int num)
    {
        monsterNum = num;
    }

    static public void DieMonster()
    {
        monsterNum--;
        if (monsterNum == 0)
            isClear.Invoke(roomPos);
    }

}
