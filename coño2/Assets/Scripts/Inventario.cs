using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    public class Inventario : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryUI;
        [SerializeField] private Transform itemsParent;
        [SerializeField] public SlotInventario[] slots;

        private List<GameObject> items = new List<GameObject>();

        private void Start()
        {
            slots = itemsParent.GetComponentsInChildren<SlotInventario>();
            UpdateUI();
        }

        public void AddItem(GameObject item)
        {
            items.Add(item);
            UpdateUI();
        }
    	public GameObject GetItem(int index)
        {

        return items[index];
    }

        public void RemoveItem(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                items.RemoveAt(index);
                UpdateUI();
            }
        }


public int GetItemsCount()
{
    return items.Count;
}

        private void UpdateUI()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < items.Count)
                {
                    slots[i].AddItem(items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }

}