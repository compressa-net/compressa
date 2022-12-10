using System.ComponentModel;

namespace Compressa.GUI.Models
{
    public enum OrderType
    {
        [Description("Dine In")]
        DineIn,
        [Description("Carry Out")]
        CarryOut,
        [Description("Delivery")]
        Delivery
    }
}

