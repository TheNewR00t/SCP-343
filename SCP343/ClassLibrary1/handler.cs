using CustomRolesCrimsonBreach.API.CustomRole;
using CustomRolesCrimsonBreach.API.Extension;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using Logger = LabApi.Features.Console.Logger;

namespace SCP343
{
    public class handler
    {
        public static void OnRoundStaring(RoundStartingEventArgs ev)
        {
            MEC.Timing.CallDelayed(3f, () =>
            {
                bool Spawnable = IsSpawnable();
                if (Spawnable && Server.PlayerCount >= Main.instance.Config.UsersNeeded)
                {
                    List<Player> players = new List<Player>();
                    foreach (Player p in Player.List.Where(x => x.IsAlive && x.Role == Main.instance.Config.PlayerToConvertRole))
                    {
                        players.Add(p);
                    }

                    Player player = players.RandomItem();
                    PlayerCustomRole rolePlayer = new PlayerCustomRole(player);

                    if (rolePlayer.IsCustomRole)
                    {
                        rolePlayer.RemoveRole();
                    }

                    CustomRole.GetRole(343).AddRole(player);

                    Logger.Debug($"Spawned random players {player.Nickname}", Main.instance.Config.debug);
                }
                else
                {
                    return;
                }
            });
        }

        public static bool IsSpawnable()
        {
            if (UnityEngine.Random.value <= Main.instance.Config.Porcentaje / 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
