using Newtonsoft.Json;

namespace Coditech.Admin.Utilities
{
    public class DecimalConverter : JsonConverter<Decimal?>
    {
        /// <summary>
        /// Writes the given Decimal? value to the given JsonWriter, normalizing it if necessary.
        /// </summary>
        /// <param name="writer">The JsonWriter to write to.</param>
        /// <param name="value">The Decimal? value to write.</param>
        /// <param name="serializer">The JsonSerializer to use.</param>
        public override void WriteJson(JsonWriter writer, Decimal? value, JsonSerializer serializer)
        {
            writer.WriteValue(Normalize(value));
        }

        /// <summary>
        /// Reads a JSON value and converts it to a Decimal? type.
        /// </summary>
        /// <returns>
        /// The Decimal? value of the JSON value.
        /// </returns>
        public override Decimal? ReadJson(JsonReader reader, Type objectType, Decimal? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return reader.Value == null ? null : Decimal.Parse(reader.Value.ToString());
        }

        /// <summary>
        /// Normalizes a decimal value by dividing it by a very small number.
        /// </summary>
        /// <param name="value">The decimal value to normalize.</param>
        /// <returns>The normalized decimal value.</returns>
        public decimal? Normalize(decimal? value)
        {
            return value ?? value / 1.000000000000000000000000000000000m;
        }
    }
}
