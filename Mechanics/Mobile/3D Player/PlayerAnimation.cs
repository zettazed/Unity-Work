using UnityEngine;

public class PlayerAnimation : PlayerInput
{
    [Header("Components")]
    public Animator Anim;

    protected override void Update()
    {
        base.Update();
        if (x != 0 || z != 0)
            Anim.SetBool("Run", true);
        else
            Anim.SetBool("Run", false);
    }
}