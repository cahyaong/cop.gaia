// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fontManager.cs" company="nGratis">
//   The MIT License (MIT)
// 
//   Copyright (c) 2014 - 2015 Cahya Ong
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
// <creation_timestamp>Saturday, 15 August 2015 12:28:48 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Mono
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;

    [Export(typeof(IManager))]
    internal class FontManager : BaseManager, IFontManager
    {
        private readonly IDictionary<string, SpriteFont> spriteFontLookup = new Dictionary<string, SpriteFont>();

        protected ContentManager ContentManager { get; private set; }

        public void Initialize(ContentManager contentManager)
        {
            Guard.AgainstNullArgument(() => contentManager);

            this.ContentManager = contentManager;
        }

        public SpriteFont GetSpriteFont(string font)
        {
            var spriteFont = default(SpriteFont);

            if (!this.spriteFontLookup.TryGetValue(font, out spriteFont))
            {
                spriteFont = this.ContentManager.Load<SpriteFont>(font);
                this.spriteFontLookup.Add(font, spriteFont);
            }

            return spriteFont;
        }
    }
}