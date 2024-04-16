using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Authentication.WebApi
{
    /// <summary>
    /// Provides basic interface for all derived classes
    /// </summary>
    public abstract class BaseMiddleware
    {
        /// <summary>
        /// The request delegate
        /// </summary>
        protected RequestDelegate Next { get; private init; }

        /// <summary>
        /// The constructor for creating the instance
        /// </summary>
        /// <param name="next">The request delegate</param>
        public BaseMiddleware(RequestDelegate next) 
        {
            this.Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Invokes this middleware component
        /// </summary>
        /// <param name="context">The context describes current HTTP request</param>
        public abstract Task Invoke(HttpContext context);
    }
}