using System.Collections;
using UnityEngine;


namespace MultiplayerARPG
{
    public interface ICharacterBlockingComponent
    {

        bool IsBlocking { get; }
        //float LastBlockEndTime { get; }
        float MoveSpeedRateWhileBlocking { get; }

        void CancelBlock();

        void Block(bool isBlocking);

    }
}