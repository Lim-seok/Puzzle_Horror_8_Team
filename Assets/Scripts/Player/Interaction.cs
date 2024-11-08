using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

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
    private Camera _camera;
    public GameObject heldItem;
    private FixedJoint fixedJoint;

    public event Action<bool> OnHoldEvent;

    private void Start()
    {
        _camera = Camera.main;
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
            promptText.text = "E키: 옮기기";
            promptText.gameObject.SetActive(true);
        }
    }

    private void PerformRaycast()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
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
            Vector3 holdPosition = transform.position + transform.forward * 0.5f + Vector3.up * 1.0f - transform.right * 0.7f;
            item.transform.position = holdPosition;

            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = rb;

            OnHoldEvent?.Invoke(true);
            Collider itemCollider = item.GetComponent<Collider>();
            if (itemCollider != null)
            {
                itemCollider.isTrigger = true;
            }
        }
    }
    public void DropItem()
    {
        if (heldItem != null)
        {
            
            Vector3 dropPosition = transform.position + transform.forward * 0.1f + Vector3.up * 1.0f;
            heldItem.transform.position = dropPosition;

            Collider itemCollider = heldItem.GetComponent<Collider>();
            Rigidbody itemRb = heldItem.GetComponent<Rigidbody>();
            
            if (itemRb != null)
            {
                // Rigidbody가 있다면 키네마틱 모드 해제
                itemRb.isKinematic = false;
                // Collider 활성화
                itemCollider.isTrigger = false;
                Destroy(fixedJoint);
                fixedJoint = null;

                // 앞쪽으로 힘을 가함
                Vector3 throwForce = transform.forward * 30f + Vector3.up * 2f; // 힘의 크기를 조절할 수 있습니다
                itemRb.AddForce(throwForce, ForceMode.Impulse);
            } 

            OnHoldEvent?.Invoke(false);
        }
        heldItem = null;
    }
}
