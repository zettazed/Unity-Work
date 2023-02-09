using UnityEngine;
using SimpleInputNamespace;

public class PlayerInput : PlayerState
{
    [Header("Components")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] protected Joystick _joystick;

    [Header("Options")]
    public float MoveSpeed = 10f;

    [SerializeField] protected float x = 0;
    [SerializeField] protected float z = 0;

    protected override void Update()
    {
        x = _joystick.Value.x * MoveSpeed * Time.deltaTime;
        z = _joystick.Value.y * MoveSpeed * Time.deltaTime;
        _controller.Move(new Vector3(x, 0, z));
    }
}