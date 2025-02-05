using UnityEngine;
using UnityEngine.UI;


    public class SlotInventario : MonoBehaviour
    {
        public Image icon;
        private GameObject item;

        public void AddItem(GameObject newItem)
        {
            item = newItem;
            InfoItem itemData = newItem.GetComponent<InfoItem>();

            if (itemData != null && itemData.inventorySprite != null)
            {
                icon.sprite = itemData.inventorySprite;
                icon.enabled = true;
            }
            else
            {
                Debug.LogWarning("No InfoItem or inventorySprite found on " + newItem.name);
                icon.enabled = false;
            }
        }

        public void ClearSlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
        }

        public void UseItem(Transform HandTransform)
        {
            if (item != null)
            {
                item.SetActive(true);
                AttachItemToHand(HandTransform);
            }
        }

public void AttachItemToHand(Transform HandTransform)
{
    if (item != null)
    {
        item.transform.SetParent(HandTransform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        Collider itemCollider = item.GetComponent<Collider>();
        if (itemCollider != null)
        {
            itemCollider.enabled = false;
        }
    }
}
        public void DestroyItem()
        {
            if (item != null)
            {
                ClearSlot();
            }
        }
}