using System.Linq;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using PlayerRoles;

namespace PersistentOverwatch.EventHandlers;

public class RoundRestartHandler : CustomEventsHandler {
  public override void OnServerRoundRestarted() {
    var overwatchPlayerIds =
      from player in Player.List
      where player.Role == RoleTypeId.Overwatch
      where player.HasPermission(PlayerPermissions.ForceclassSelf)
      select player.UserId;
    
    PersistentOverwatch.Instance.OverwatchIds.UnionWith(overwatchPlayerIds); 
  }
}
