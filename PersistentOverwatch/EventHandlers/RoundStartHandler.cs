using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using PlayerRoles;

namespace PersistentOverwatch.EventHandlers;

public class RoundStartHandler : CustomEventsHandler {
  public override void OnServerRoundStarted() {
    // give all players in the list the Overwatch role
    foreach(string userId in PersistentOverwatch.Instance.OverwatchIds) {
      if (!Player.TryGet(userId, out Player player)) continue;

      player.SetRole(RoleTypeId.Overwatch, RoleChangeReason.RoundStart);
    }
    
    PersistentOverwatch.Instance.OverwatchIds.Clear();
  }
}
