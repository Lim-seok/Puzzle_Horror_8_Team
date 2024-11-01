using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;
    private GameObject heldItem;

    public float rotationSpeed = 10;
    private float currentRotation = 0f;
    private const float MaxRotation = 30f;


    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        switch (data. type)
        {
            case ItemType.Held:
                //들기타입 아이템처리
                if (heldItem == null)
                {
                    PickUpItem(gameObject);
                }
                else
                {
                    DropItem();
                }
                break;

            case ItemType.Rotation:
                //회전타입 아이템처리
                RotateObject();
                break;

            default:
                Debug.Log($"Interraing{data.displayName}");
                break;
        }       
    }

    private void DropItem()
    {
        if (heldItem != null)
        {
            heldItem.transform.SetParent(null);

            // 물리 효과 다시 활성화
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.zero; // 초기 속도 리셋
            }

            // 아이템을 플레이어 앞에 놓기
            heldItem.transform.position = transform.position + transform.forward * 1f;

            heldItem = null;
        }
    }
    public void PickUpItem(GameObject item)
    {
        if (heldItem == null)
        {
            heldItem = item;
            item.transform.SetParent(transform);

            // 플레이어 바로 앞 z축에 아이템 위치시키기
            item.transform.localPosition = new Vector3(0, 0, 1f);
            item.transform.localRotation = Quaternion.identity;

            // 물리 효과 비활성화
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

        }
    }

    public void RotateObject()
    {
        if(currentRotation < MaxRotation)
        {
            // 남은 회전 각도 계산
            float remainingRotation = MaxRotation - currentRotation;
            // 이번 프레임에서 회전할 각도 계산 (최대 회전 각도를 초과하지 않도록)
            float rotationAmount = Mathf.Min(rotationSpeed * Time.deltaTime, remainingRotation);

            // 아이템 회전
            transform.Rotate(Vector3.up, rotationAmount);
            // 현재 회전 각도 업데이트
            currentRotation += rotationAmount;
        }
        else
        {
            // 최대 회전에 도달하면 초기 위치로 리셋
            transform.rotation = Quaternion.identity;
            currentRotation = 0f;
        }
    }
}
