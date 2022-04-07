using System.Collections;
using UnityEngine;


using MultiplayerARPG.GameData.Model.Playables;

using System.Collections.Generic;

using Cysharp.Threading.Tasks;
using LiteNetLib;
using LiteNetLibManager;

using System.Threading;

//Credit: TidyDev for the example

//Example call from ShooterController
//  PlayerCharacterEntity.BlockingComponent?.Block(!PlayerCharacterEntity.IsSheathed && GetSecondaryAttackButton());

namespace MultiplayerARPG
{
    public class CharacterBlockingComponent : BaseNetworkedGameEntityComponent<BaseCharacterEntity>, ICharacterBlockingComponent
    {

        [Header("Rather Good Blocking Component")]

        [SerializeField] bool isBlocking;
        public bool IsBlocking { get { return isBlocking; } private set { isBlocking = value; } }

        //[SerializeField] float lastBlockEndTime;
       // public float LastBlockEndTime { get { return lastBlockEndTime; } private set { lastBlockEndTime = value; } }

        [SerializeField] float moveSpeedRateWhileBlocking;
        public float MoveSpeedRateWhileBlocking { get { return moveSpeedRateWhileBlocking; } private set { moveSpeedRateWhileBlocking = value; } }

        protected List<CancellationTokenSource> blockCancellationTokenSources = new List<CancellationTokenSource>();

        PlayableCharacterModel_Custom playableCharacterModel_Custom;

        [Header("Debug ME")]

        [SerializeField] WeaponType weaponTypeBlock;

        [SerializeField] bool isLeftHand;


        public override void EntityAwake()
        {
            playableCharacterModel_Custom = (PlayableCharacterModel_Custom)Entity.Model;

        }

        public void Block(bool isBlocking)
        {

            if (isBlocking == IsBlocking)
                return;

            if (isBlocking)
                BlockStaminaCheck(); // Decrease stamina

            RPC(AllPlayBlockAnimation, BaseCharacterEntity.ACTION_TO_CLIENT_DATA_CHANNEL, DeliveryMethod.ReliableOrdered, isBlocking);

            //CallAllPlayBlockAnimation(isBlocking); // Broadcast block animation playing
        }

        //public bool CallAllPlayBlockAnimation(bool isBlocking)
        //{
        //    RPC(AllPlayBlockAnimation, BaseCharacterEntity.ACTION_TO_CLIENT_DATA_CHANNEL, DeliveryMethod.ReliableOrdered, isBlocking);
        //    return true;
        //}

        [AllRpc]
        protected void AllPlayBlockAnimation(bool isBlocking)
        {
            //if (IsOwnerClientOrOwnedByServer)
            //    return;

            IsBlocking = isBlocking;

            if (isBlocking)
            {
                //Entity.GetCaches().MoveSpeed = MoveSpeedRateWhileBlocking;

                isLeftHand = true; //Default blocking is left hand if available
                weaponTypeBlock = Entity.GetAvailableWeapon(ref isLeftHand).GetWeaponItem().WeaponType;
                playableCharacterModel_Custom.StartBlock(weaponTypeBlock);
            }
            else
            {
                CancelBlock();
            }

        }

        public void CancelBlock()
        {
            IsBlocking = false;
            playableCharacterModel_Custom.CancelBlocking();
        }

        public override void EntityStart()
        {

        }

        public override void EntityOnDestroy()
        {

        }

        public override void EntityUpdate()
        {

        }

        private void BlockStaminaCheck()
        {
            if (!Entity.IsServer) return;

            if (Entity.IsRecaching) return;

            if (Entity.IsDead()) return;

            //if (!tidyGameplayRule) tidyGameplayRule = CurrentGameplayRule as TidyGameplayRule;

            //// Check if can block
            //if (Entity.CurrentStamina >= (int)tidyGameplayRule.GetDecreasingStaminaWhileBlocking(Entity))
            //{
            //    Entity.CurrentStamina += -(int)tidyGameplayRule.GetDecreasingStaminaWhileBlocking(Entity);

            //    // Start block routine
            //    IsBlocking = true;
            //}

            //IsBlocking = true;
        }



    }



}