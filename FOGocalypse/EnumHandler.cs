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
            Carpet
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
            PistolAmmo
        }

        public enum FurnitureTypes
        {
            Couch,
            Table,
            Chair,
            Bed,
            SmallTable
        }

        public enum WeatherType
        {
            Sunny,
            Cloudy,
            Rainy
        }

        public enum PlantTypes
        {
            Tree
        }
    }
}
