using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : PuzzleBase, IInteractable
{
    public ItemData data;
    private Interaction interaction;
    private float currentRotation = 0f;
    public float rotationDuration = 1f;
    private ButtonController buttonController;

    void Start()
    {
        interaction = FindAnyObjectByType<Interaction>();
        buttonController = GetComponent<ButtonController>();
    }
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}: {data.description}";
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

            case ItemType.Button:
                // ��ư Ÿ�� ������ ó��
                if (buttonController != null)
                {
                    buttonController.OnButtonInteract();
                }
                break;

            default:
                Debug.Log($"Interraing{data.displayName}");
                break;
        }       
    }


    public void RotateObject()
    {
        StartCoroutine(RotateObjectCoroutine());
    }

    private IEnumerator RotateObjectCoroutine()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, 30f, 0f);
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rotationDuration;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        // ��Ȯ�� ���� ��ġ ����
        transform.rotation = targetRotation;
    }

    public void InteractSwitch()
    {
        SetPuzzleState(!CheckState());
    }
}

