using Content.Shared.Imperial.Medieval.Factions.Prototypes;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared.Imperial.Medieval.CollegiumAi;

/// <summary>
/// An incorporeal wisp a player pilots around the island. It cannot touch anything. It exists to watch the
/// Collegium's own mages, talk to them, and strip the spells off any that misbehave.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class CollegiumAiComponent : Component
{
    /// <summary>
    /// The statue this watcher is bound to. Destroying it banishes the watcher.
    /// </summary>
    [ViewVariables]
    public EntityUid? Core;

    /// <summary>
    /// Faction the watcher may follow. Everyone outside it is invisible to the watcher and cannot be targeted.
    /// </summary>
    [DataField]
    public ProtoId<MedievalFactionPrototype> Faction = "Collegium";

    /// <summary>
    /// How far the watcher may drift from the nearest living mage of <see cref="Faction"/> before it gets reeled in.
    /// </summary>
    [DataField]
    public float LeashRange = 16f;

    /// <summary>
    /// How long the watcher is allowed to stay outside <see cref="LeashRange"/> before it is pulled back.
    /// Stops a mage walking briskly away from yanking the watcher around.
    /// </summary>
    [DataField]
    public TimeSpan LeashGrace = TimeSpan.FromSeconds(6);

    /// <summary>
    /// When the current out-of-range stretch started. Null while the watcher is within range.
    /// </summary>
    [ViewVariables]
    public TimeSpan? LeashSince;

    #region Actions

    [DataField]
    public EntProtoId TeleportAction = "ActionMedievalCollegiumAiTeleport";

    [DataField]
    public EntProtoId StripMagicAction = "ActionMedievalCollegiumAiStripMagic";

    [DataField]
    public EntProtoId WhisperAction = "ActionMedievalCollegiumAiWhisper";

    [DataField]
    public EntProtoId RestoreMagicAction = "ActionMedievalCollegiumAiRestoreMagic";

    [DataField]
    public EntProtoId ReturnAction = "ActionMedievalCollegiumAiReturn";

    [DataField]
    public EntProtoId BarrierAction = "ActionMedievalCollegiumAiBarrier";

    [ViewVariables]
    public EntityUid? TeleportActionEntity;

    [ViewVariables]
    public EntityUid? StripMagicActionEntity;

    [ViewVariables]
    public EntityUid? RestoreMagicActionEntity;

    [ViewVariables]
    public EntityUid? WhisperActionEntity;

    [ViewVariables]
    public EntityUid? ReturnActionEntity;

    [ViewVariables]
    public EntityUid? BarrierActionEntity;

    #endregion
}

/// <summary>
/// Added to mages the watcher is allowed to follow, so we know which entities we handed a vision seed to.
/// </summary>
[RegisterComponent]
public sealed partial class CollegiumAiWatchedComponent : Component;

/// <summary>
/// Records the spells the Collegium took off a mage, so a watcher can hand them back later.
/// Lives on the mage rather than the watcher, so it survives the watcher being replaced mid-round.
/// </summary>
[RegisterComponent]
public sealed partial class CollegiumAiStrippedComponent : Component
{
    /// <summary>
    /// Prototypes of the spell actions that were taken, in the order they were removed.
    /// </summary>
    [DataField]
    public List<EntProtoId> Spells = new();
}
