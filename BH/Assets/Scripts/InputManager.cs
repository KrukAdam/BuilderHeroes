
namespace BHInputManager
{
    public class InputManager
    {
        public InputController InputController { get; private set; }

        public void Init()
        {
            InputController = new InputController();
        }

        public void Enable()
        {
            InputController.Enable();
        }

        public void Disable()
        {
            InputController.Disable();
        }
    }

}
