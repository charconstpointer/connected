using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Connected.Api.Validation
{
    public class PipelineValidator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator> _validators;

        public PipelineValidator(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var validationFailures = _validators
                .Select(validator => validator.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();

            if (validationFailures.Any())
            {
                var error = string.Join("\r\n", validationFailures);
                throw new MyCustomValidationException(validationFailures.Select(x => x.ErrorMessage));
            }

            return next();
        }
    }

    public sealed class MyCustomValidationException : Exception
    {
        public MyCustomValidationException(IEnumerable<string> errors)
        {
            Data["Errors"] = errors;
        }
    }
}