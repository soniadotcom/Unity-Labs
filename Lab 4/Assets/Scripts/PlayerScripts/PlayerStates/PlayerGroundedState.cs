using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : IPlayerState
{
    public void updatePlayerState(IPlayer player)
    {
        if (Input.GetButtonDown("Jump"))
        {
            player.Jump();

            player.SetState(new PlayerJumpedState());
        }
    }
}
