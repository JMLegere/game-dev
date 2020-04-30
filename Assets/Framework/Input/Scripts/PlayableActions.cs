using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayableActions : PlayerActionSet
{
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Jump;
    public PlayerOneAxisAction Move;

    public PlayableActions()
    {
        Left = CreatePlayerAction( "Move Left" );
        Right = CreatePlayerAction( "Move Right" );
        Jump = CreatePlayerAction( "Jump" );
        Move = CreateOneAxisPlayerAction( Left, Right );
    }
}