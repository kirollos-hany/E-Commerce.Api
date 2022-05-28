using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Api.Validation;

public class Result
{
    private static readonly Result _success = new() { Succeeded = true };
    private readonly List<ResultError> _errors = new();

    public bool Succeeded { get; private set; }

    public static Result Success
    {
        get => _success;
    }

    public IEnumerable<ResultError> Errors
    {
        get => _errors;
    }

    public static Result Failed(params ResultError[] errors)
    {
        var result = new Result { Succeeded = false };
        result._errors.AddRange(errors);
        return result;
    }

    public static Result GetPortfolioResult(IdentityResult result)
    {
        return result.Succeeded ? Success : Failed(result.Errors.ToArray());
    }

    public static Result Failed(params IdentityError[] errors)
    {
        var result = new Result { Succeeded = false };
        foreach (var err in errors)
        {
            result._errors.Add(new ResultError { Code = err.Code, Description = err.Description });
        }
        return result;
    }

    public override string ToString()
    {
        return string.Join('\n', Errors.Select(e => e.Description));
    }
}