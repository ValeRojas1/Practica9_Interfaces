using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 3.0f;
    [SerializeField] private LayerMask interactionMask = ~0; // por defecto, todo
    [SerializeField] private Camera playerCamera; // referenciar la Main Camera

    private PlayerInputActions _input;

    private void Awake()
    {
        _input = new PlayerInputActions();
        if (playerCamera == null)
            playerCamera = Camera.main; // cachear referencia (costoso en runtime)
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        // Solo intentamos interactuar cuando el jugador presiona E
        if (_input.Player.Interact.triggered)
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        if (playerCamera == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactionMask, QueryTriggerInteraction.Ignore))
        {
            // Buscar cualquier componente que implemente el contrato IInteractable
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                return;
            }
        }

        // Si llegamos aquí, no había nada interactuable
        Debug.Log("No hay nada con lo que interactuar.");
    }
}
