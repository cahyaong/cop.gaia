// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFontManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 15 August 2015 12:33:54 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Mono
{
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using nGratis.Cop.Gaia.Engine;

    public interface IFontManager : IManager
    {
        void Initialize(ContentManager contentManager);

        SpriteFont GetSpriteFont(string font);
    }
}