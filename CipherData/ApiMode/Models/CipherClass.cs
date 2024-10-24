using System.Text.Json;

namespace CipherData.ApiMode
{
    public abstract class CipherClass : ICipherClass
    {
        public string ToJson()
            => JsonSerializer.Serialize(this, GetType(), ICipherClass.JsonOptions);

        public bool Equals<T>(T? otherObject) where T : ICipherClass
        {
            if (otherObject is null) return false;
            string thisJson = ToJson();
            string otherJson = otherObject.ToJson();
            return thisJson == otherJson;
        }
    }
}
