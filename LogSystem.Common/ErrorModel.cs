namespace LogSystem.Common
{
    public class ErrorModel
    {
        public string code;
        public string description;

        public ErrorModel(string code, string description)
        {
            this.code = code;
            this.description = description;
        }
    }
}
