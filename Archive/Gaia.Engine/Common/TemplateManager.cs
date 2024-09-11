// --------------------------------------------------------------------------------------------------------------------
// <copyright file="templateManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 4 August 2015 12:56:30 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine.Core;

    [Export(typeof(IManager))]
    public class TemplateManager : BaseManager, ITemplateManager
    {
        private readonly IDictionary<uint, ITemplate> templateLookup = new Dictionary<uint, ITemplate>();

        public void AddTemplate(ITemplate template)
        {
            RapidGuard.AgainstNullArgument(template);
            Guard.AgainstInvalidArgument(string.IsNullOrEmpty(template.Name), () => template);

            this.templateLookup.Add(template.Id, template);
        }

        public void RemoveTemplate(string name)
        {
            Guard.AgainstNullOrWhitespaceArgument(() => name);

            var template = this.FindTemplate(name);
            this.templateLookup.Remove(template.Id);
        }

        public ITemplate FindTemplate(string name)
        {
            Guard.AgainstNullOrWhitespaceArgument(() => name);

            return this
                .templateLookup
                .Values
                .Single(value => value.Name == name);
        }

        public ITemplate FindTemplate(uint id)
        {
            return this.templateLookup[id];
        }
    }
}