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
                //들기타입 아이템처리
                if (interaction.heldItem == null)
                {
                    interaction.PickUpItem(gameObject);
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

    public void RotateObject()
    {
        // 현재 회전 각도에 30도 추가
        currentRotation += rotationAmount;

        // 360도를 넘어가면 0으로 리셋
        if (currentRotation >= 360f)
        {
            currentRotation = 0f;
        }

        // 오브젝트를 Y축 기준으로 회전
        transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);
    }
}
