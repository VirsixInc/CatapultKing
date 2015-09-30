using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{

    public enum FireShotState
    {
        Idle,
        UserPulling,
        BallFlying
    }

    public enum GameState
    {
        Start,
        BallMovingtoPos,
        Playing,
        Won,
        Lost
    }


    public enum BallState
    {
        BeforeThrown,
        Thrown
    }

}
