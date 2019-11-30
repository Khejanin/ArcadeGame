using Character;

namespace Game
{
    public class InputManager
    {
        public string inputHString;
        public string inputVString;
        public string shoot;
        public string useAbility;
        public string useItem;

        public InputManager(PlayerController.PlayerEnum player)
        {
            if (player == PlayerController.PlayerEnum.PlayerRight)
            {
                inputHString = "HorizontalP2";
                inputVString = "VerticalP2";
                shoot = "FireP2";
                useAbility = "AbilityP2";
                useItem = "ItemP2";
            }
            else
            {
                inputHString = "HorizontalP1";
                inputVString = "VerticalP1";
                useAbility = "AbilityP1";
                shoot = "FireP1";
                useItem = "ItemP1";
            }
        }
    }
}