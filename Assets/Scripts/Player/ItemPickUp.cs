using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public GameObject heldItem;

    public void PickUpItem(GameObject item)
    {
        if (heldItem == null)
        {
            heldItem = item;

            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }
            item.transform.SetParent(transform);

            // 플레이어 바로 앞 z축에 아이템 위치시키기
            item.transform.localPosition = new Vector3(0, 1f, 1f);
            item.transform.localRotation = Quaternion.identity;
        }
    }
    public void DropItem()
    {   
        Rigidbody rb = heldItem.AddComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        heldItem.transform.SetParent(null);

        heldItem = null;   
    }
}
