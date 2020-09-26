using System;
using System.Reflection;

namespace IVAX.INTERCONEXIONBANCARIA.COMPLETA.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}