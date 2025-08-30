using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRadius; //상호작용 반경

    private PlayerInputAction playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Interaction.Enable();

        playerInputAction.Interaction.Interact.performed += (InputAction.CallbackContext c) => TryInteract();
    }

    private void OnDestroy()
    {
        playerInputAction.Interaction.Disable();
    }

    //상호작용 시도
    private void TryInteract()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, interactionRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact(transform);
                break;
            }
        }
    }
}
