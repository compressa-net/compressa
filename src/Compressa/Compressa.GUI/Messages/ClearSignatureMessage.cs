using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Compressa.GUI.Messages;

public class ClearSignatureMessage : ValueChangedMessage<bool>
{
    public ClearSignatureMessage(bool value) : base(value)
    {
    }
}

