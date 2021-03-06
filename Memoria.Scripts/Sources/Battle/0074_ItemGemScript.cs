using System;

namespace Memoria.Scripts.Battle
{
    /// <summary>
    /// Gems: Garnet, Amethyst, Aquamarine, Diamond, Emerald, Moonstone, Ruby, Peridot, Sapphire, Opal, Topaz, Lapis Lazuli, Ore
    /// </summary>
    [BattleScript(Id)]
    public sealed class ItemGemScript : IBattleScript, IEstimateBattleScript
    {
        public const Int32 Id = 0074;

        private readonly BattleCalculator _v;

        public ItemGemScript(BattleCalculator v)
        {
            _v = v;
        }

        public void Perform()
        {
            _v.Target.Flags |= CalcFlag.HpAlteration | CalcFlag.HpRecovery;
            Byte itemId = (Byte)_v.Command.AbilityId;
            _v.Target.HpDamage = (Int16)(_v.Command.Item.Power * (ff9item.FF9Item_GetCount(itemId) + 1));
        }

        public Single RateTarget()
        {
            Byte itemId = (Byte)_v.Command.AbilityId;
            Int16 recovery = (Int16)(_v.Command.Item.Power * (ff9item.FF9Item_GetCount(itemId) + 1));

            Single rate = recovery * BattleScriptDamageEstimate.RateHpMp(_v.Target.CurrentHp, _v.Target.MaximumHp);
            
            if (!_v.Target.IsPlayer)
                rate *= -1;

            return rate;
        }
    }
}