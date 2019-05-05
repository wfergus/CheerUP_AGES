
/// <summary>
/// interface for elements can interact with by pressing the interact button
/// </summary>
public interface IInteractable
{
    string DisplayText { get; }

    void InteractWith();
    
}
