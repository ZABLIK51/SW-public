using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Shared.Imperial.Medieval.Calendar;

[Prototype("calendarEvent")]
public sealed partial class CalendarEventPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField("texture")]
    public string Texture { get; set; } = string.Empty;

    [DataField("name")]
    public string Name { get; set; } = string.Empty;

    [DataField("text")]
    public string Text { get; set; } = string.Empty;

    [DataField("sound")]
    public SoundSpecifier? Sound { get; set; }

    [DataField("tags")]
    public HashSet<string> Tags { get; private set; } = new();

    /// <summary>
    /// Вес. Чем выше - тем больше шанс появления этого дня
    /// </summary>
    [DataField("weight")]
    public float Weight { get; private set; } = 1.0f;

    /// <summary>
    /// Максимальное количество таких дней в раунде
    /// </summary>
    [DataField("maxOccurrences")]
    public int MaxOccurrences { get; private set; } = 1;

    /// <summary>
    /// Не раньше какого дня может выпасть этот день
    /// </summary>
    [DataField("minDay")]
    public int MinDay { get; private set; } = 1;

    /// <summary>
    /// Минимальное количество дней до повторного выпадения ивента.
    /// 1 = может выпасть на следующий день.
    /// 2 = перерыв минимум 1 день.
    /// 3 = перерыв минимум 2 дня и т.д.
    /// </summary>
    [DataField("minOffset")]
    public int MinOffset { get; private set; } = 1;
}

public enum CalendarDayType : byte
{
    Day,
    Night
}
