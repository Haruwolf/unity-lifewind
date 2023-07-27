namespace GameSystem.Inputs.Interfaces
{
    public interface IInputEvents
    {
        public void OnStarted();
        public void OnHold();
        public void OnPerformed();
        public void OnCanceled();
    }
}

