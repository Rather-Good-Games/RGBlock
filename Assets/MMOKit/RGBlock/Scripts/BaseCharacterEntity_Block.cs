
using MultiplayerARPG.GameData.Model.Playables;
using System.Collections;
using UnityEngine;

using LiteNetLib;
using LiteNetLibManager;
using System.Collections.Generic;


namespace MultiplayerARPG
{
    public partial class BaseCharacterEntity
    {

        public ICharacterBlockingComponent BlockingComponent { get; protected set; }


        [DevExtMethodsAttribute("Awake")]
        protected void SetupCombat()
        {
            BlockingComponent = gameObject.GetOrAddComponent<ICharacterBlockingComponent, CharacterBlockingComponent>();
        }


    }
}