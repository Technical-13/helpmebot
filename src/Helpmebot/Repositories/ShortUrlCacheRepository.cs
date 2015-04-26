﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShortUrlCacheRepository.cs" company="Helpmebot Development Team">
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

namespace Helpmebot.Repositories
{
    using System;
    using System.Linq;

    using Castle.Core.Logging;

    using Helpmebot.Model;
    using Helpmebot.Repositories.Interfaces;

    using NHibernate;
    using NHibernate.Criterion;

    /// <summary>
    /// The short url cache repository.
    /// </summary>
    public class ShortUrlCacheRepository : RepositoryBase<ShortUrlCacheEntry>, IShortUrlCacheRepository
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ShortUrlCacheRepository"/> class.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public ShortUrlCacheRepository(ISession session, ILogger logger)
            : base(session, logger)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get by long url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="ShortUrlCacheEntry"/>.
        /// </returns>
        [Obsolete]
        public ShortUrlCacheEntry GetByLongUrl(string url)
        {
            return this.Get(Restrictions.Eq("LongUrl", url)).FirstOrDefault();
        }

        /// <summary>
        /// The get short url.
        /// </summary>
        /// <param name="longUrl">
        /// The long url.
        /// </param>
        /// <param name="cacheMissCallback">
        /// The cache miss callback.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetShortUrl(string longUrl, Func<string, string> cacheMissCallback)
        {
            string result = longUrl;
            try
            {
                this.Transactionally(
                    session =>
                        {
                            this.Logger.DebugFormat("Searching cache for {0}", longUrl);
                            var shortUrlCacheEntry =
                                session.CreateCriteria<ShortUrlCacheEntry>()
                                    .Add(Restrictions.Eq("LongUrl", longUrl))
                                    .List<ShortUrlCacheEntry>()
                                    .FirstOrDefault();
                        
                            if (shortUrlCacheEntry == null)
                            {
                                this.Logger.DebugFormat("Cache MISS for {0}", longUrl);

                                string shortUrl = cacheMissCallback(longUrl);

                                shortUrlCacheEntry = new ShortUrlCacheEntry { LongUrl = longUrl, ShortUrl = shortUrl };
                                session.SaveOrUpdate(shortUrlCacheEntry);
                                result = shortUrlCacheEntry.ShortUrl;
                            }
                            else
                            {
                                this.Logger.DebugFormat("Cache HIT for {0}", longUrl);
                                result = shortUrlCacheEntry.ShortUrl;
                            }
                        });
            }
            catch (Exception e)
            {
                Logger.Error("Error encountered resolving URL", e);
            }

            return result;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(true);
        }

        #endregion
    }
}