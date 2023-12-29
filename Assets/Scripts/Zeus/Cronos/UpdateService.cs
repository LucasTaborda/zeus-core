namespace ZeusCore.Cronos
{
    public class UpdateService
    {
        public delegate void UpdateEvent();

        public event UpdateEvent OnUpdate;
        public event UpdateEvent OnFixedUpdate;
        public event UpdateEvent OnLateUpdate;

        public bool active;

        public void Update()
        {
            if (!active) return;

            OnUpdate?.Invoke();
        }


        public void FixedUpdate()
        {
            if (!active) return;

            OnFixedUpdate?.Invoke();
        }


        public void LateUpdate()
        {
            if (!active) return;

            OnLateUpdate?.Invoke();
        }
    }
}
