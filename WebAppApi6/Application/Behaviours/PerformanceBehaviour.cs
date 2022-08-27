//using System;
//using System.Diagnostics;
//using System.Threading;
//using System.Threading.Tasks;
//using Application.Common.Interfaces;
//using MediatR;
//using Microsoft.Extensions.Logging;

//namespace Application.Common.Behaviours
//{
//    /// <summary>
//    /// Specifies class for handling incoming requests performance.
//    /// </summary>
//    /// <typeparam name="TRequest">Type of incomming request.</typeparam>
//    /// <typeparam name="TResponse">Type of the response.</typeparam>
//    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//    {
//        private readonly Stopwatch _timer;
//        private readonly ILogger<TRequest> _logger;
//        private readonly ICurrentUserService _currentUserService;
//        private readonly IIdentityService _identityService;

//        /// <summary>
//        /// Initializes performance behaviour for incomming requests.
//        /// </summary>
//        /// <param name="logger">Logging service.</param>
//        /// <param name="currentUserService">Service to manage current user.</param>
//        /// <param name="identityService">Service to manage user identity.</param>
//        public PerformanceBehaviour(
//            ILogger<TRequest> logger,
//            ICurrentUserService currentUserService,
//            IIdentityService identityService)
//        {
//            _timer = new Stopwatch();

//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
//            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
//        }

//        /// <summary>
//        /// Handles performance of incoming request.
//        /// </summary>
//        /// <param name="request">Incoming request.</param>
//        /// <param name="cancellationToken">Cancellation token.</param>
//        /// <param name="next">Next pipeling begaviour.</param>
//        /// <returns>Response for the incomming request.</returns>
//        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
//        {
//            _timer.Start();

//            var response = await next();

//            _timer.Stop();

//            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

//            if (elapsedMilliseconds > 500)
//            {
//                var requestName = typeof(TRequest).Name;
//                var userId = _currentUserService.UserId ?? string.Empty;
//                var userName = string.Empty;

//                if (!string.IsNullOrEmpty(userId))
//                {
//                    userName = await _identityService.GetUserNameAsync(userId);
//                }

//                _logger.LogWarning("Tool Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
//                    requestName, elapsedMilliseconds, userId, userName, request);
//            }

//            return response;
//        }
//    }
//}