using UnityEngine;

public class TerminalController : MonoBehaviour, IInteractable
{
    [SerializeField] private Light terminalLight;
    private bool _isActive;

    private void Reset()
    {
        if (terminalLight == null)
            terminalLight = FindAnyObjectByType<Light>(); // opcional, mejor arrastrar
    }

    public void Interact()
    {
        _isActive = !_isActive;
        if (terminalLight != null)
        {
            terminalLight.color = _isActive ? Color.green : Color.red;
        }
        Debug.Log($"Estado del sistema: {(_isActive ? "Activo" : "Inactivo")}");
    }
}
