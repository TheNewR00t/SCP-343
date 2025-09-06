using PlayerRoles;
using System.ComponentModel;
using UnityEngine;

namespace SCP343
{
    public class Config
    {
        [Description("Defines the scale of the player (X, Y, Z).")]
        public Vector3 PlayerScale { get; set; } = new Vector3(1, 1, 1);

        [Description("Percentage to spawn.")]
        public float Porcentaje { get; set; } = 20f;

        [Description("Number of players required to trigger this role.")]
        public int UsersNeeded { get; set; } = 6;

        [Description("The role that will be converted into SCP-343.")]
        public RoleTypeId PlayerToConvertRole { get; set; } = RoleTypeId.ClassD;

        public string NoPlayers { get; set; } = "No players alive right now";

        [Description("Enable or disable debug mode.")]
        public bool debug { get; set; } = false;

    }
}
