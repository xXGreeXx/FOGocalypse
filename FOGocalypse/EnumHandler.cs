namespace FOGocalypse
{
    public class EnumHandler
    {
        public enum TileTypes
        {
            Grass,
            Dirt,
            Stone,
            Wood,
            Carpet,
            TilledDirt,
        }

        public enum Directions
        {
            Up,
            Right,
            Down,
            Left
        }

        public enum GameStates
        {
            Game,
            GameSettingsMenu,
            MainMenu,
            OptionsMenu
        }

        public enum Items
        {
            None,
            Flashlight,
            Waterbottle,
            Emptybottle,
            Knife,
            Peanutbutter,
            Bread,
            Pistol,
            PistolAmmo,
            Berry
        }

        public enum FurnitureTypes
        {
            Couch,
            Table,
            Chair,
            Bed,
            SmallTable,
            Counter,
            Sink,
            Oven,
            Door,
            Shelf
        }

        public enum WeatherType
        {
            Sunny,
            Cloudy,
            Rainy
        }

        public enum SeasonType
        {
            Winter,
            Spring,
            Summer,
            Fall
        }

        public enum PlantTypes
        {
            Tree,
            Bush
        }
    }
}
