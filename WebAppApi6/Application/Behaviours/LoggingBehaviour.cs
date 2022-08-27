//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Application.Common.Interfaces;
//using MediatR.Pipeline;
//using Microsoft.Extensions.Logging;

//namespace Application.Common.Behaviours
//{
//    /// <summary>
//    /// Represents behaviour for Request logging.
//    /// </summary>
//    /// <typeparam name="TRequest">Type of Mediator request.</typeparam>
//    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
//    {
//        private readonly ILogger _logger;
//        private readonly ICurrentUserService _currentUserService;
//        private readonly IIdentityService _identityService;

//        /// <summary>
//        /// Defines <see cref="LoggingBehaviour{TRequest}" entity./>
//        /// </summary>
//        /// <param name="logger">Logging service.</param>
//        /// <param name="currentUserService"Service to manage current users.></param>
//        /// <param name="identityService">Service for checking user identity.</param>
//        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IIdentityService identityService)
//        {
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
//            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
//        }

//        /// <summary>
//        /// Process request before calling the Handler.
//        /// </summary>
//        /// <param name="request">Incoming request.</param>
//        /// <param name="cancellationToken">Cancellation token.</param>
//        /// <returns>An awaitable task.</returns>
//        public async Task Process(TRequest request, CancellationToken cancellationToken)
//        {
//            var requestName = typeof(TRequest).Name;
//            var userId = _currentUserService.UserId ?? string.Empty;
//            string userName = string.Empty;

//            if (!string.IsNullOrEmpty(userId))
//            {
//                userName = await _identityService.GetUserNameAsync(userId);
//            }

//            _logger.LogInformation("Request: {Name} {@UserId} {@UserName} {@Request}", requestName, userId, userName, request);
//        }
//    }
//}