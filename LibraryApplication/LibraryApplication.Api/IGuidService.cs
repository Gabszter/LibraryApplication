// <copyright file="IGuidService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryApplication.Api
{
    /// <summary>
    /// Represents a service for generating GUIDs.
    /// </summary>
    public interface IGuidService
    {
        /// <summary>
        /// Gets the generated GUID.
        /// </summary>
        Guid Guid { get; }
    }
}
