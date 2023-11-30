using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BrickType {
    
        IceCream,
        Pizza,
        Burger,
        Escimo,
        Watermelon,
        Peach,
        Cherry,
        Apple,
        Avocado,
        Strawberry,
        Lollipop,
        LollipopCandy,
        Donut,
        Fries,
        Muffin,
        Cake,
        Cookie,
        Coffee,
        SandwichWithEggs,
        CupOfCoffee,
        Onigiri,
        SushiRoll,
        Narutomaki,
        Ramen,
        Mochi,
        Pancakes,
        Wafer,
        Croissant,
        BentoCake,
        Jam
    }
}
