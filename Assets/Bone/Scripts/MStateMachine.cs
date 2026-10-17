using UnityEngine;

public class MStateMachine : MonoBehaviour
{
   
    public MState CurrentState { get; private set; }

    public void SwitchState(MState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
    private void Update()
    {
        CurrentState?.Tick();
    }
}
