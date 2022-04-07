# RGBlock

**Description:** 

 Sheild/Weapon block for MMORPG Kit

 Example code for USUPPORTED MOD use at your own risk. Not a complete or working project by itself.
 
**Credits** Special thanks to https://github.com/benhamlett aka TidyDev

**Author:** RatherGood1

**Version**: 0.1: 7 Apr 22

**Example code**

Possible example can use modified ShooterPlayerCharacterController replace RMB with thi line to activate/deactivate block.

```csharp 
PlayerCharacterEntity.BlockingComponent?.Block(!PlayerCharacterEntity.IsSheathed && GetSecondaryAttackButton()); 
```