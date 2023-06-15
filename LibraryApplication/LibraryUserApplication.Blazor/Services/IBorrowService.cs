// <copyright file="IBorrowService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace LibraryUserApplication.Blazor.Services
{
    using LibraryUserApplication.Blazor.Model;
    using LibraryApp.Contract;

    /// <summary>
    /// Provides methods for managing borrows.
    /// </summary>
    public interface IBorrowService
    {
        Task<IEnumerable<BorrowInfo>?> GetBorrowByNameAsync(string name);
    }
}
