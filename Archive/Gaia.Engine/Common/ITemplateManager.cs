// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplateManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 4 August 2015 1:04:08 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    public interface ITemplateManager : IManager
    {
        void AddTemplate(ITemplate template);

        void RemoveTemplate(string name);

        ITemplate FindTemplate(string name);

        ITemplate FindTemplate(uint id);
    }
}