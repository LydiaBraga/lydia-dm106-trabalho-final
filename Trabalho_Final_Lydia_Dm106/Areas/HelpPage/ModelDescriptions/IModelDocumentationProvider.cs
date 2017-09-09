using System;
using System.Reflection;

namespace Trabalho_Final_Lydia_Dm106.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}