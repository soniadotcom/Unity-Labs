using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpedState : IPlayerState
{
    float time;
    public PlayerJumpedState()
    {
        time = Time.time;
    }
    public void updatePlayerState(IPlayer player)
    {
        if (Time.time >= time + 1f && player.RaycastFromAllSensors())
        {
            player.SetState(new PlayerGroundedState());
        }
    }
}
