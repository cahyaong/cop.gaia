// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fontManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
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