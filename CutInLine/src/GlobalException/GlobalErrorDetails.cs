using Newtonsoft.Json;

namespace CutInLine.Models.GlobalException
{
    internal class GlobalErrorDetails
    {
        public bool Success { get; internal set; }
        public string? Message { get; internal set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}