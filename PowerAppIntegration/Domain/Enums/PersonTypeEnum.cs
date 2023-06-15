using System.ComponentModel;

namespace Migration.Domain.Domain.Enums
{
    public enum PersonTypeEnum
    {
        [Description("PersonaNatural")]
        NaturalPerson,

        [Description("PersonaJuridica")]
        LegalEntiry,
    }    
}
