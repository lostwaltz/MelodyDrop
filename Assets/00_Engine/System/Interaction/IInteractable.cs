
namespace Engine
{
    public interface IInteractable
    {
        public InteractionType InteractionType { get; set; }

        public void Interact();
    }
}