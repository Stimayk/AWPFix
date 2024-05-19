using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace AWPFix
{
    public class AWPFix : BasePlugin
    {
        public override string ModuleName => "AWPFix";
        public override string ModuleVersion => "1.0.0";
        public override string ModuleAuthor => "E!N";
        public override string ModuleDescription => "Restores 10 rounds of ammo to the AWP";

        public override void OnAllPluginsLoaded(bool hotReload)
        {
            RegisterListener<Listeners.OnEntityCreated>(entity =>
            {
                if (entity?.Entity == null || !entity.IsValid ||
                    !string.Equals(entity.DesignerName, "weapon_awp", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                Server.NextFrame(() =>
                {
                    var weapon = new CBasePlayerWeapon(entity.Handle);

                    if (!weapon.IsValid) return;

                    var csWeapon = weapon.As<CCSWeaponBase>();
                    if (csWeapon == null) return;

                    if (csWeapon.VData != null)
                    {
                        csWeapon.VData.MaxClip1 = 10;
                        csWeapon.VData.DefaultClip1 = 10;
                    }

                    csWeapon.Clip1 = 10;

                    Utilities.SetStateChanged(csWeapon, "CBasePlayerWeapon", "m_iClip1");
                });
            });
        }
    }
}