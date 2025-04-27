using System.Net;

namespace Models
{
    public class APIResponse<T>
	{
		public HttpStatusCode StatusCode { get; set; }
		public T Data { get; set; }
		public string Message { get; set; }
		public List<string> Errors { get; set; }

		public APIResponse(HttpStatusCode statusCode, string message, T data)
		{
			StatusCode = statusCode;
			Message = message;
			Data = data;
			Errors = new List<string>();
		}

		public APIResponse(HttpStatusCode statusCode, string message, T data, List<string> errors)
		{
			StatusCode = statusCode;
			Message = message;
			Data = data;
			Errors = errors;
		}
	}
}