using UnityEngine;

public class LootChestController : MonoBehaviour, IInteractable
{
    [SerializeField] private bool hasLoot = true;
    private bool _isOpen;

    public void Interact()
    {
        if (_isOpen)
        {
            Debug.Log("Cofre: ya está abierto.");
            return;
        }

        _isOpen = true;
        if (hasLoot)
        {
            Debug.Log("Cofre: ¡Has encontrado un tesoro!");
            hasLoot = false; // lo reclamas una vez
        }
        else
        {
            Debug.Log("Cofre: está vacío...");
        }
    }
}
