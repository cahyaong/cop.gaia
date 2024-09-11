// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisualStudioHook.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Friday, 29 April 2016 12:21:31 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System;
    using System.Globalization;
    using SyntaxTree.VisualStudio.Unity.Bridge;
    using UnityEditor;

    [InitializeOnLoad]
    public class VisualStudioHook
    {
        private const string RootNamespace = "nGratis.Cop.Gaia.Client.Unity";

        static VisualStudioHook()
        {
            ProjectFilesGenerator.ProjectFileGeneration += VisualStudioHook.OnProjectFileGenerated;
        }

        private static string OnProjectFileGenerated(string name, string content)
        {
            return content.Replace(
                "<RootNamespace></RootNamespace>",
                string.Format(
                    CultureInfo.InvariantCulture,
                    "<RootNamespace>{0}</RootNamespace>",
                    VisualStudioHook.RootNamespace));
        }
    }
}