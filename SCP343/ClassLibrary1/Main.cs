using CustomRolesCrimsonBreach.Events;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using System;

namespace SCP343
{
    public class Main : Plugin<Config>
    {
        public override string Name => "SCP343";

        public override string Description => "add SCP343 in to SCP SL";

        public override string Author => "davilone32";

        public override Version Version => new Version(1,0,0);

        public override Version RequiredApiVersion => LabApiProperties.CurrentVersion;

        public static Main instance { get; set; }

        public override void Enable()
        {
            instance = this;
            CustomRoleHandler.RegisterRoles();
        }

        public override void Disable()
        {
            CustomRoleHandler.UnRegisterRoles();
            instance = null;
        }
    }
}
