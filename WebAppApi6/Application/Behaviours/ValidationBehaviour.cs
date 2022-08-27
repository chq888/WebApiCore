using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviours
{
    /// <summary>
    /// Represents behaviour on Request validation.
    /// </summary>
    /// <typeparam name="TRequest">Type of Mediator request.</typeparam>
    /// <typeparam name="TResponse">Type of Mediator response.</typeparam>
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Defines <see cref="ValidationBehaviour{TRequest, TResponse}" entity./>
        /// </summary>
        /// <param name="validators"></param>
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        /// <summary>
        /// Handle request validation.
        /// </summary>
        /// <param name="request">Requet to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="next">Next pipeline behaviour.</param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(vr => vr.Errors).Where(er => er != null).ToList();

                if (failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}