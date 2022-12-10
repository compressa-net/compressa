using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Compressa.GUI.Messages;

public class SaveSignatureMessage : ValueChangedMessage<int>
{
    public SaveSignatureMessage(int value) : base(value)
    {
    }
}

