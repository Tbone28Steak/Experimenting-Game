public abstract class MState
{
    protected PlayerMovementManager MovementManager;
    protected PlayerInputManager InputManager;
    protected PlayerCameraManager CameraManager;
    protected MStateMachine StateMachine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public MState(PlayerMovementManager MovementManager, PlayerInputManager InputManager, PlayerCameraManager CameraManager, MStateMachine StateMachine)
    {
    this.MovementManager = MovementManager;
    this.InputManager = InputManager;
    this.CameraManager = CameraManager;
    }

   public virtual void Enter() { }
   public virtual void Tick() { }
   public virtual void Exit() { }
}
