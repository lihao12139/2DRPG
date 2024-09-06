using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayControl player;
    void Start()
    {
        player = GetComponentInParent<PlayControl>();
    }

    private void AnimationTrigger()
    {
        player.AttackOver();
    }
}
