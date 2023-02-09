using UnityEngine;

public class PlayerRotation : PlayerAnimation
{
    [Header("Options")]
    public Transform PlayerModel;
    [SerializeField] private float _speedRotation = 10f;
    [SerializeField] private float y = 50f;

    protected override void Update()
    {
        base.Update();
        if ((_joystick.Value.y != 0 || _joystick.Value.x != 0))
        {
            if (_joystick.Value.y > 0)
                PlayerModel.rotation = Quaternion.Euler(x: -90, y: _joystick.Value.x * 90f, z: 0);
            else
                PlayerModel.rotation = Quaternion.Euler(x: -90, y: (_joystick.Value.x * -90f) + 180f, z: 0);
        }
    }
}