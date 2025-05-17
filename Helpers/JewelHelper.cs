using ArenaBuildsMod.Models;
using ProjectM.Network;
using Stunlock.Core;

namespace ArenaBuildsMod.Helpers;

internal static class JewelHelper
{
    public static void CreateAndEquip(User user, PrefabGUID abilityGuid, JewelData jewelData)
    {
        var createjevelevent = new CreateJewelDebugEventV2
        {
            AbilityPrefabGuid = abilityGuid,
            Equip = true,
            SpellMod1 = UtilsHelper.GetPrefabGuid(jewelData.SpellMod1) ?? default,
            SpellMod1Power = jewelData.SpellMod1Power,
            SpellMod2 = UtilsHelper.GetPrefabGuid(jewelData.SpellMod2) ?? default,
            SpellMod2Power = jewelData.SpellMod2Power,
            SpellMod3 = UtilsHelper.GetPrefabGuid(jewelData.SpellMod3) ?? default,
            SpellMod3Power = jewelData.SpellMod3Power,
            SpellMod4 = UtilsHelper.GetPrefabGuid(jewelData.SpellMod4) ?? default,
            SpellMod4Power = jewelData.SpellMod4Power,
            Tier = 3
        };

        Core.DebugEventsSystem.CreateJewelEvent(user.Index, ref createjevelevent);
    }
}