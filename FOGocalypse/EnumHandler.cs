namespace FOGocalypse
{
    public class EnumHandler
    {
        public enum TileTypes
        {
            Grass,
            Dirt,
            Stone,
            Wood
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
            Chair
        }
    }
}
