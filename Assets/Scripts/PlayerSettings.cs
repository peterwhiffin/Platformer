using UnityEngine;

[CreateAssetMenu(menuName = "Player/Settings", fileName = "NewPlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    public float MoveSpeed;
    public float JumpForce;
    public float LinearDrag;
    public float GroundCheckDistance;
    public float StopForce;
    public float InAirMoveSpeed;
    public LayerMask GroundMask;
    public float MaxSpeed;
    public float GravityAdd;
    public float MaxJumpTime;
    public float MaxJumpSpeed;
}
