using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;


namespace MultiplayerARPG.GameData.Model.Playables
{
    public partial class PlayableCharacterModel_Custom : PlayableCharacterModel
    {

        [ArrayElementTitle("blockWeaponType")]
        public BlockAnimations[] blockAnimations;

        [NonSerialized] public Dictionary<WeaponType, BlockAnimations> blockAnimationDict = new Dictionary<WeaponType, BlockAnimations>();

        [NonSerialized] private bool initializedBlockDict = false;

        Coroutine blockingActionRoutineRef = null;

        public bool StartBlock(WeaponType weaponType, float blockDuration = 30f)
        {
            InitBockingAnimationsDict();

            if (blockAnimationDict.TryGetValue(weaponType, out BlockAnimations tempBlockAnimations))
            {
                tempBlockAnimations.blockAnimation.extendDuration = blockDuration; //block should last longer, TODO: Something else?
                blockingActionRoutineRef = (PlayActionAnimationDirectly(tempBlockAnimations.blockAnimation));
                return true;
            }

            return false;

        }

        public void CancelBlocking()
        {
            if (blockingActionRoutineRef != null)
            {
                CancelPlayingActionAnimationDirectly();
                blockingActionRoutineRef = null;
            }
        }


        public void InitBockingAnimationsDict()
        {
            if (initializedBlockDict)
                return;

            blockAnimationDict.Clear();

            if (blockAnimations != null && blockAnimations.Length > 0)
            {
                foreach (BlockAnimations item in blockAnimations)
                {
                    blockAnimationDict[item.BlockWeaponType] = item;
                }
            }

            initializedBlockDict = true;
        }


    }
}