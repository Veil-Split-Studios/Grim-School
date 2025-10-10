using Unity.VisualScripting;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    private InventoryBar _invBar;
    private InventoryManager _invMan;
    [SerializeField]
    private Transform _handPoint;


    private void Awake()
    {
        _invBar = FindObjectOfType<InventoryBar>();
        _invMan = FindObjectOfType<InventoryManager>();
    }

    private void OnEnable()
    {
        _invBar.OnSlotChanged += UpdateHandItem;
    }

    private void OnDisable()
    {
        _invBar.OnSlotChanged -= UpdateHandItem;
    }

    private void UpdateHandItem(int newIndex)
    {
        if (_handPoint.transform.childCount > 0)
        {
            Transform currentItem = _handPoint.transform.GetChild(0);
            currentItem.transform.SetParent(null);
            currentItem.gameObject.SetActive(false);
        }

        newIndex = _invBar.selectedIndex;

        if (_invMan.items[newIndex] != null)
        {
            ItemData newSo = _invMan.items[newIndex];
            GameObject newItem = newSo.GetComponent<GameObject>().gameObject;
            newItem.SetActive(true);
            newItem.transform.SetParent(_handPoint.transform);
            newItem.transform.localPosition = Vector3.zero;
            newItem.transform.rotation = Quaternion.identity;
        }
        else if(_invBar.slots[newIndex] == null)
        {
            //currentItem = null;
            _handPoint = null;
        }


    }

}
