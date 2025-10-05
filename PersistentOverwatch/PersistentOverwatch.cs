using System;
using System.Collections.Generic;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using PersistentOverwatch.EventHandlers;

namespace PersistentOverwatch;

internal class PersistentOverwatch : Plugin {
  public override string Name { get; }                = "PersistentOverwatch";
  public override string Description { get; }         = "Makes the Overwatch role persist on round restarts.";
  public override string Author { get; }              = "git4rker";
  public override Version Version { get; }            = new(1, 0, 0);
  public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);

  public static PersistentOverwatch Instance { get; private set; }
  
  // stores the UserIDs of players that should be granted
  // the Overwatch role on round start
  public HashSet<string> OverwatchIds = new();
  
  private RoundRestartHandler _roundRestartHandler = new();
  private RoundStartHandler _roundStartHandler = new();

  public override void Enable() {
    Instance = this;

    CustomHandlersManager.RegisterEventsHandler(_roundRestartHandler);
    CustomHandlersManager.RegisterEventsHandler(_roundStartHandler);
  }

  public override void Disable() {
    CustomHandlersManager.UnregisterEventsHandler(_roundStartHandler);
    CustomHandlersManager.UnregisterEventsHandler(_roundRestartHandler);
   
    Instance = null;
  }
}
