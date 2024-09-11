// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateManagerExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 6 August 2015 1:56:27 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using nGratis.Cop.Gaia.Engine.Core;

    public static class TemplateManagerExtensions
    {
        public static void LoadCreatureTemplates(this ITemplateManager templateManager)
        {
            RapidGuard.AgainstNullArgument(templateManager);

            templateManager.AddTemplate(
                new Template(
                    0x10000000,
                    "Character",
                    new BrainComponent(),
                    new StatisticComponent(),
                    new ConstitutionComponent(),
                    new TraitComponent(),
                    new PlacementComponent(),
                    new PhysicsComponent()));
        }
    }
}