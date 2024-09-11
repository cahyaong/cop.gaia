// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntity.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 5 August 2015 2:11:33 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    public interface IEntity
    {
        uint Id { get; }

        uint OwnerId { get; set; }

        uint TemplateId { get; }
    }
}