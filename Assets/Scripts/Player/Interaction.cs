using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}
public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;
    public GameObject heldItem;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            if (heldItem == null)
            {
                PerformRaycast();
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
        if (heldItem != null)
        {
            promptText.text = "놓기\nE키를 누르세요.";
            promptText.gameObject.SetActive(true);
        }
    }

    private void PerformRaycast()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        {
            if (hit.collider.gameObject != curInteractGameObject)
            {
                curInteractGameObject = hit.collider.gameObject;
                curInteractable = hit.collider.GetComponent<IInteractable>();
                SetPromptText();
            }
        }
        else
        {
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (heldItem != null)
            {
                DropItem();
                promptText.gameObject.SetActive(false);
            }
            else if (curInteractable != null && heldItem == null)
            {
                curInteractable.OnInteract();
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }
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
