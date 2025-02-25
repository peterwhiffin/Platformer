using UnityEngine;
namespace PetesPlatformer
{
    [CreateAssetMenu(menuName = "Settings/Player/MovementSettings", fileName = "NewPlayerMovementSettings")]
    public class PlayerMovementSettings : ScriptableObject 
    {
        [field: SerializeField] public float MaxMoveSpeed { get; private set; }
        [field: SerializeField] public float MaxJumpTime { get; private set; }
        [field: SerializeField] public float GroundAcceleration { get; private set; }
        [field: SerializeField] public float AirAcceleration { get; private set; }
        [field: SerializeField] public float AirDrag { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public float Gravity { get; private set; }
        [field: SerializeField] public float GroundCheckRadius { get; private set; }
        [field: SerializeField] public float WallCheckRadius { get; private set; }
        [field: SerializeField] public float WallCheckOffset { get; private set; }
        [field: SerializeField] public float WallSlideSpeed { get; private set; }
        [field: SerializeField] public LayerMask GroundMask { get; private set; }
        [field: SerializeField] public float VariableJumpVelocityMultiplier { get; private set; }
    }
}
