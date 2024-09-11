// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Entity.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 1 August 2015 1:49:11 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    public class Entity : IEntity
    {
        public Entity(uint id, uint templateId)
            : this(id, 0, templateId)
        {
        }

        public Entity(uint id, uint ownerId, uint templateId)
        {
            this.Id = id;
            this.OwnerId = ownerId;
            this.TemplateId = templateId;
        }

        public uint Id { get; private set; }

        public uint OwnerId { get; set; }

        public uint TemplateId { get; private set; }
    }
}