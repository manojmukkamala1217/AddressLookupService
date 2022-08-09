namespace Api.Library.Models
{
	public class ErrorResult
	{
		public string? Code { get; }
		public string Message { get; }
		public IEnumerable<Error> Errors { get; }

		public ErrorResult(string? code, string message, IEnumerable<Error> errors)
		{
			Code = code;
			Message = message;
			Errors = errors;
		}

		public ErrorResult(string message, IEnumerable<Error> errors) : this(null, message, errors)
		{
		}

		public ErrorResult(string code, string message) : this(code, message, Enumerable.Empty<Error>())
		{
		}		
	}

	public class Error
	{
		public string Message { get; }
		public string? Field { get; }

		public Error(string message) : this(null, message)
		{

		}

		public Error(string? field, string message)
		{
			Message = message;
			Field = field != string.Empty ? field : null;
		}		
	}
}
