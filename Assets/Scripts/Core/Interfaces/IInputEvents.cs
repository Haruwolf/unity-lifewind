namespace Core.Interfaces
{
    public interface IInputEvents
    {
        public void OnHold();
        public void OnPerformed();
        public void OnCanceled();
    }
}

