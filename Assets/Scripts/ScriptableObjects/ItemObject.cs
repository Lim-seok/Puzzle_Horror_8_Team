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
                //���Ÿ�� ������ó��
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
                //ȸ��Ÿ�� ������ó��
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

            // ���� ȿ�� �ٽ� Ȱ��ȭ
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.zero; // �ʱ� �ӵ� ����
            }

            // �������� �÷��̾� �տ� ����
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

            // �÷��̾� �ٷ� �� z�࿡ ������ ��ġ��Ű��
            item.transform.localPosition = new Vector3(0, 0, 1f);
            item.transform.localRotation = Quaternion.identity;

            // ���� ȿ�� ��Ȱ��ȭ
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
            // ���� ȸ�� ���� ���
            float remainingRotation = MaxRotation - currentRotation;
            // �̹� �����ӿ��� ȸ���� ���� ��� (�ִ� ȸ�� ������ �ʰ����� �ʵ���)
            float rotationAmount = Mathf.Min(rotationSpeed * Time.deltaTime, remainingRotation);

            // ������ ȸ��
            transform.Rotate(Vector3.up, rotationAmount);
            // ���� ȸ�� ���� ������Ʈ
            currentRotation += rotationAmount;
        }
        else
        {
            // �ִ� ȸ���� �����ϸ� �ʱ� ��ġ�� ����
            transform.rotation = Quaternion.identity;
            currentRotation = 0f;
        }
    }
}
