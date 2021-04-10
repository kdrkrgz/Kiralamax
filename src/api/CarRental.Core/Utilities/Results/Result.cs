namespace CarRental.Core.Utilities.Results
{
    public class Result: IResult
    {
        public Result(bool isSucces, string message):this(isSucces)
        {
            Message = message;
        }

        public Result(bool isSucces)
        {
            IsSuccess = isSucces;
        }

        public bool IsSuccess { get; }
        public string Message { get; }
    }
}
