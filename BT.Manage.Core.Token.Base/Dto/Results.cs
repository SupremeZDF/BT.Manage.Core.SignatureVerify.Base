namespace BT.Manage.AspNet.Token.Base
{
    public class Result<T> where T : class, new()
    {
        public int code { get; set; }

        public string message { get; set; }

        public T @object { get; set; }
    }
}
