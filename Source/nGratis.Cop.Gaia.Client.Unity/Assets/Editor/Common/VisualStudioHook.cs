// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisualStudioHook.cs" company="nGratis">
//   The MIT License (MIT)
//
//   Copyright (c) 2014 - 2016 Cahya Ong
//
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
//   associated documentation files (the "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the
//   following conditions:
//
//   The above copyright notice and this permission notice shall be included in all copies or substantial
//   portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
//   LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO
//   EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//   IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR
//   THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Friday, 29 April 2016 12:21:31 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
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
                "<RootNamespace></RootNamesace>",
                string.Format(
                    CultureInfo.InvariantCulture,
                    "<RootNamespace>{0}</RootNamesace>",
                    VisualStudioHook.RootNamespace));
        }
    }
}