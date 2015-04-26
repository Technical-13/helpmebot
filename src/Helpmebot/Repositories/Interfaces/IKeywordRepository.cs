﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IKeywordRepository.cs" company="Helpmebot Development Team">
//   Helpmebot is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   Helpmebot is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//   
//   You should have received a copy of the GNU General Public License
//   along with Helpmebot.  If not, see http://www.gnu.org/licenses/ .
// </copyright>
// <summary>
//   Defines the IKeywordRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Helpmebot.Repositories.Interfaces
{
    using System.Collections.Generic;

    using Helpmebot.Model;

    /// <summary>
    /// The KeywordRepository interface.
    /// </summary>
    public interface IKeywordRepository : IRepository<Keyword>
    {
        /// <summary>
        /// The get by name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="Keyword"/>.
        /// </returns>
        IEnumerable<Keyword> GetByName(string name);

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        void Create(string name, string response, bool action);
    }
}
