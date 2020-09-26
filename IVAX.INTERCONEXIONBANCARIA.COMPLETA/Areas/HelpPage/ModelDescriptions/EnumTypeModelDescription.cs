using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IVAX.INTERCONEXIONBANCARIA.COMPLETA.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}