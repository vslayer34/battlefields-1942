using BattleField1942.Scripts.Helper.DataClasses;
using Battlefields1942.Scripts.Helper.Enums;
using Godot;
using Godot.Collections;
using System;

public partial class StanceHandler : Node
{
    [ExportCategory("Required")]
    [Export]
    public CollisionShape3D CharacterCollisionShape { get; private set; }

    [ExportCategory("")]
    [ExportGroup("Stances Configs")]
    [Export]
    private Dictionary<StanceType, StanceVariables> _stanceConfigs;
    
    private StanceType _activeStance;




    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        // SwitchStance(StanceType.Standing);
    }



    // Member Methods------------------------------------------------------------------------------

    public void SwitchStance(StanceType stance)
    {
        ActiveStance = stance;
    }

    private void UpdateCollisionShape(StanceType stance)
    {
        GD.Print("Called");
        GD.Print($"Capsule Shape Radius: {CapsuleShape.Radius}");
        GD.Print($"Stance Configs: {_stanceConfigs[stance].ColliderRadius}");


        CapsuleShape.Radius = _stanceConfigs[stance].ColliderRadius;
        GD.Print($"Capsule Shape Radius: {CapsuleShape.Radius}");

        CapsuleShape.Height = _stanceConfigs[stance].ColliderHeight;

        CharacterCollisionShape.Position = new Vector3(ColliderPivot.X, _stanceConfigs[stance].ColliderPivot, ColliderPivot.Z);
    }

    // Getters and Setters-------------------------------------------------------------------------

    private CapsuleShape3D CapsuleShape => CharacterCollisionShape.Shape as CapsuleShape3D;
    private Vector3 ColliderPivot => CharacterCollisionShape.Position;
    public StanceType ActiveStance
    {
        get => _activeStance;
        set
        {
            _activeStance = value;
            UpdateCollisionShape(_activeStance);
        }
    }
}