namespace CarRental.Core.Utilities.Results
{
    public class DataResult<T>: Result, IDataResult<T>
    {
        public DataResult(T data,bool isSucces, string message) : base(isSucces, message)
        {
            Data = data;
        }

        public DataResult(T data, bool isSucces) : base(isSucces)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
