﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationHelper.cs" company="Helpmebot Development Team">
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
// --------------------------------------------------------------------------------------------------------------------
namespace Helpmebot.Configuration
{
    using System.Configuration;

    using Helpmebot.Configuration.XmlSections.Interfaces;
    using Helpmebot.Services.Interfaces;

    /// <summary>
    ///     The configuration helper.
    /// </summary>
    public class ConfigurationHelper : IConfigurationHelper
    {
        #region Fields

        /// <summary>
        /// The core configuration.
        /// </summary>
        private ICoreConfiguration coreConfiguration;

        /// <summary>
        /// The database configuration.
        /// </summary>
        private IDatabaseConfiguration databaseConfiguration;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the core configuration.
        /// </summary>
        public ICoreConfiguration CoreConfiguration
        {
            get
            {
                return this.coreConfiguration
                       ?? (this.coreConfiguration = ConfigurationManager.GetSection("core") as ICoreConfiguration);
            }
        }

        /// <summary>
        ///     Gets the database configuration.
        /// </summary>
        /// <returns>
        ///     The <see cref="DatabaseConfiguration" />.
        /// </returns>
        public IDatabaseConfiguration DatabaseConfiguration
        {
            get
            {
                return this.databaseConfiguration
                       ?? (this.databaseConfiguration =
                           ConfigurationManager.GetSection("database") as IDatabaseConfiguration);
            }
        }

        #endregion
    }
}