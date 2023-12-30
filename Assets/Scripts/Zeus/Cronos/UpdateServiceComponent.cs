using UnityEngine;

namespace ZeusCore.Cronos
{
    public class UpdateServiceComponent : MonoBehaviour
    {
        public UpdateService updateService;
        public bool active = true;


        private void Awake()
        {
            updateService = new UpdateService();

            Locator.AddService(updateService);
        }


        public void Update()
        {
            updateService.Update();
        }


        public void FixedUpdate()
        {
            updateService.FixedUpdate();
        }


        public void LateUpdate()
        {
            updateService.LateUpdate();
        }


        public void OnDestroy()
        {
            Locator.RemoveService(updateService);
        }
    }
}
