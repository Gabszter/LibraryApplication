// <copyright file="GuidService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryApplication.Api
{
    /// <summary>
    /// Represents a service for generating GUIDs.
    /// </summary>
    public class GuidService : IGuidService
    {
        /// <summary>
        /// Gets the generated GUID.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidService"/> class.
        /// </summary>
        public GuidService()
        {
            this.Guid = Guid.NewGuid();
        }
    }
}
