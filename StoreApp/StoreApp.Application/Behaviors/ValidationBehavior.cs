using FluentValidation;
using MediatR;
using ValidationException = StoreApp.Application.Exceptions.ValidationException;

namespace StoreApp.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    var errorsDictionary = failures
                        .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                        .ToDictionary(
                            failureGroup => failureGroup.Key,
                            failureGroup => failureGroup.ToArray()
                        );

                    // 3. Ném Exception của bạn (StoreApp.Application.Exceptions)
                    throw new ValidationException(errorsDictionary);
                }
            }
            return await next();
        }
    }
}
