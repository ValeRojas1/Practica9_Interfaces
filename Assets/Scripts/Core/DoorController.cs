// Assets/Scripts/Core/DoorController.cs
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float speed = 5f;

    private bool _isOpen;
    private float _targetAngle;

    private void Reset()
    {
        if (doorPivot == null) doorPivot = transform;
    }

    private void Update()
    {
        if (doorPivot == null) return;
        Quaternion target = Quaternion.Euler(0f, _targetAngle, 0f);
        doorPivot.localRotation = Quaternion.Slerp(doorPivot.localRotation, target, Time.deltaTime * speed);
    }

    public void Interact()
    {
        _isOpen = !_isOpen;
        _targetAngle = _isOpen ? openAngle : 0f;
        Debug.Log(_isOpen ? "Puerta: ABIERTA" : "Puerta: CERRADA");
    }
}
