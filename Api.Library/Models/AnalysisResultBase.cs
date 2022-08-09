namespace Api.Library.Models
{
	public class AnalysisResultBase
	{
		public bool IsSuccess { get; set; }
		public string? Message { get; set; }
		public List<string> FailureReasons { get; } = new List<string>();
	}
}
