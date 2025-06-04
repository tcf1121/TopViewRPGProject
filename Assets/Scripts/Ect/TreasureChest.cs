using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private int _floor;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<GameObject> _items;
    // 가지고 있는 아이템 리스트

    void OnDestroy()
    {
        RoomManager.DieMonster();
        DropItem();
    }

    public void SetFloor(int floor)
    {
        _floor = floor;
    }

    public void DropItem()
    {
        int dropRandom = Random.Range(0, _items.Count);
        GameObject itemObj = Instantiate(_items[dropRandom]);
        itemObj.name = _items[dropRandom].GetComponent<Item>().ItemCode;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick"))
            _animator.SetTrigger("IsOpen");
    }

    void DestroyTreasure()
    {
        Destroy(gameObject);
    }
}
