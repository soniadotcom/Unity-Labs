using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    bool RaycastFromAllSensors();
    void Jump();
    void SetState(IPlayerState playerState);
}
