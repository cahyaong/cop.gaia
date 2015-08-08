// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateManager.cs" company="nGratis">
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
    using nGratis.Cop.Gaia.Engine.Core;

    public class TemplateManager : ITemplateManager
    {
        private readonly IDictionary<string, ITemplate> templates = new Dictionary<string, ITemplate>();

        public ITemplate FindTemplate(string name)
        {
            Guard.AgainstNullOrWhitespaceArgument(() => name);

            var template = default(ITemplate);

            Guard.AgainstInvalidOperation(!this.templates.TryGetValue(name, out template));

            return template;
        }

        public void AddTemplate(ITemplate template)
        {
            Guard.AgainstNullArgument(() => template);

            this.templates.Add(template.Name, template);
        }

        public void RemoveTemplate(string name)
        {
            Guard.AgainstNullArgument(() => name);
            Guard.AgainstInvalidOperation(!this.templates.ContainsKey(name));

            this.templates.Remove(name);
        }
    }
}