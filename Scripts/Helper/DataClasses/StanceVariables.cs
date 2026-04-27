using Godot;

namespace BattleField1942.Scripts.Helper.DataClasses;
[GlobalClass]
public partial class StanceVariables : Resource
{
    [Export]
    public float ColliderHeight { get; private set; } = 2.0f;

    [Export]
    public float ColliderRadius { get; private set; } = 0.5f;

    [Export]
    public float ColliderPivot { get; private set; } = 1.0f;
}