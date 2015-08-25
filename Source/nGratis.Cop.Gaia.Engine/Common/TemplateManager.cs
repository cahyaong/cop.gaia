// --------------------------------------------------------------------------------------------------------------------
// <copyright file="templateManager.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 - 2015 Cahya Ong
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 4 August 2015 12:56:30 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System.Collections.Generic;
    using System.Linq;
    using nGratis.Cop.Gaia.Engine.Core;

    public class TemplateManager : ITemplateManager
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
            Guard.AgainstNullOrEmptyArgument(() => name);

            var template = this.FindTemplate(name);
            this.templateLookup.Remove(template.Id);
        }

        public ITemplate FindTemplate(string name)
        {
            Guard.AgainstNullOrEmptyArgument(() => name);

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