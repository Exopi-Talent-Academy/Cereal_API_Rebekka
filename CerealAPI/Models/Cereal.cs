using Cereal_API.Models.Types;

namespace Cereal_API.Models;

/// <summary>
/// A cereal's data in the system.
/// </summary>
/// <param name="Id">The unique identifier of the cereal</param>
/// <param name="Name">The name of the cereal</param>
/// <param name="Mfr">The manufacturer of the cereal</param>
/// <param name="Type">The type of the cereal (hot or cold)</param>
/// <param name="Calories">The number of calories per serving in the cereal</param>
/// <param name="Protein">The grams of protein in the cereal</param>
/// <param name="Fat">The grams of fat in the cereal</param>
/// <param name="Sodium">The milligrams of sodium in the cereal</param>
/// <param name="Fiber">The grams of fiber in the cereal</param>
/// <param name="Carbo">The grams of carbohydrates in the cereal</param>
/// <param name="Sugars">The grams of sugars in the cereal</param>
/// <param name="Potass">The milligrams of potassium in the cereal</param>
/// <param name="Vitamins">The percentage of vitamins and minerals in the cereal</param>
/// <param name="Shelf">The display shelf location of the cereal</param>
/// <param name="Weight">The weight in ounces of one serving of the cereal</param>
/// <param name="Cups">The number of cups in one serving of the cereal</param>
/// <param name="Rating">The rating of the cereal</param>
public record Cereal(Guid Id,
                     string Name,
                     Manufacturers Mfr,
                     HotOrColdType Type,
                     int Calories,
                     int Protein,
                     int Fat,
                     int Sodium,
                     float Fiber,
                     float Carbo,
                     int Sugars,
                     int Potass,
                     VitaminAndMineralsType Vitamins, //int in original dataset
                     DisplayShelfType Shelf,         //int in original dataset
                     float Weight,
                     float Cups,
                     float Rating);
