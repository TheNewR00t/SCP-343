using CustomRolesCrimsonBreach.API.CustomRole;
using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using LabApi.Loader;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SCP343
{
    public class Role : CustomRole
    {
        public override string Name => "SCP343";

        public override string CustomInfo => Name;

        public override uint Id => 343;

        public override RoleTypeId BaseRole => RoleTypeId.Tutorial;

        public override float SpawnPorcentage => 0;

        public override Vector3 Scale { get; set; } = Vector3.one;

        public override List<string> Inventory { get; set; } = new List<string>() 
        {
            $"{ItemType.Flashlight}",
            $"{ItemType.Coin}"
        };

        public override void EventsCustom()
        {
            LabApi.Events.Handlers.PlayerEvents.DroppedItem += OnExitedItem;
            LabApi.Events.Handlers.PlayerEvents.PickedUpItem += OnGrabItem;
            LabApi.Events.Handlers.PlayerEvents.DroppingItem += OnExitingItem;

            base.EventsCustom();
        }

        public override void UnEventsCustom()
        {
            LabApi.Events.Handlers.PlayerEvents.DroppedItem -= OnExitedItem;
            LabApi.Events.Handlers.PlayerEvents.DroppingItem -= OnExitingItem;
            LabApi.Events.Handlers.PlayerEvents.PickedUpItem -= OnGrabItem;
            base.UnEventsCustom();
        }

        public override void AddRole(Player player)
        {
            player.IsGodModeEnabled = true;

            base.AddRole(player);
        }

        private void OnGrabItem(PlayerPickedUpItemEventArgs ev)
        {
            if (ev.Player == null) return;
            if (ev.Item == null) return;

            if (HasRole(ev.Player, this))
            {
                Pickup item = ev.Item.DropItem();
                item.Destroy();
                ev.Player.AddItem(ItemType.Medkit);
            }
        }

        private void OnExitingItem(PlayerDroppingItemEventArgs ev)
        {
            if (HasRole(ev.Player, this))
            {
                if (ev.Item.Type == ItemType.Flashlight)
                {
                    ev.Player.IsGodModeEnabled = false;
                    ev.Player.Kill();
                }

                if (ev.Item.Type == ItemType.Coin)
                {
                    List<Player> players = Player.List.Where(p => p.IsAlive && p.Team != Team.SCPs && p != ev.Player).ToList();

                    if (players.Count == 0)
                    {
                        ev.Player.SendHint(Main.instance.Config.NoPlayers);
                        return;
                    }

                    ev.Player.Position = players[UnityEngine.Random.Range(0, players.Count)].Position;
                } 
            }
        }

        private void OnExitedItem(PlayerDroppedItemEventArgs ev)
        {
            if (HasRole(ev.Player, this))
            {
                if (ev.Pickup.Type == ItemType.Flashlight)
                {
                    ev.Pickup.Destroy();
                }


                if (ev.Pickup.Type == ItemType.Coin)
                {
                    ev.Pickup.Destroy();
                    ev.Player.AddItem(ItemType.Coin);
                }
            }
        }
    }
}
