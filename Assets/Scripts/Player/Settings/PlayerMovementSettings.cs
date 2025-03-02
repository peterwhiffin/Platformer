using UnityEngine;
namespace PetesPlatformer
{
    [CreateAssetMenu(menuName = "Settings/Player/MovementSettings", fileName = "NewPlayerMovementSettings")]
    public class PlayerMovementSettings : ScriptableObject 
    {
        [field: SerializeField] public float GroundSpeed { get; private set; }
        [field: SerializeField] public float AirSpeed { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public float JumpInterval { get; private set; }
        [field: SerializeField] public float WallJumpHeight { get; private set; }
        [field: SerializeField] public float WallJumpHorizontalSpeed { get; private set; }
        [field: SerializeField] public float WallMovementThreshold { get; private set; }
        [field: SerializeField] public float VariableJumpVelocityMultiplier { get; private set; }
        [field: SerializeField] public float WallSlideSpeed { get; private set; }
        [field: SerializeField] public float Gravity { get; private set; }
        [field: SerializeField] public float TerminalVelocity { get; private set; }
        [field: SerializeField] public float GroundCheckRadius { get; private set; }
        [field: SerializeField] public float WallCheckRadius { get; private set; }
        [field: SerializeField] public float WallCheckOffset { get; private set; }
        [field: SerializeField] public float WallJumpMovementCooldown { get; private set; }
        [field: SerializeField] public LayerMask GroundMask { get; private set; }
    }
}
