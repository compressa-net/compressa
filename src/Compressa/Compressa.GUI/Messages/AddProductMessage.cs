using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Compressa.GUI.Messages;

public class AddProductMessage : ValueChangedMessage<bool>
{
    public AddProductMessage(bool value) : base(value)
    {
    }
}

