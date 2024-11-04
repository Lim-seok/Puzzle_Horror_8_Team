using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;
    private Interaction interaction;

    private float currentRotation = 0f;
    private const float rotationAmount = 30f;


    void Start()
    {
        interaction = FindObjectOfType<Interaction>();
    }
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
                if (interaction.heldItem == null)
                {
                    interaction.PickUpItem(gameObject);
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

    public void RotateObject()
    {
        // ���� ȸ�� ������ 30�� �߰�
        currentRotation += rotationAmount;

        // 360���� �Ѿ�� 0���� ����
        if (currentRotation >= 360f)
        {
            currentRotation = 0f;
        }

        // ������Ʈ�� Y�� �������� ȸ��
        transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);
    }
}
