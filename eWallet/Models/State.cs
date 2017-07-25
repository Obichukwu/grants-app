using System;

namespace eWallet.Models
{
    public enum State
    {
        Abia=1,
        Adamawa,
        AkwaIbom,
        Anambra,
        Bauchi,
        Bayelsa,
        Benue,
        Borno,
        CrossRiver,
        Delta,
        Ebonyi,
        Edo,
        Ekiti,
        Enugu,
        Fct,
        Gombe,
        Imo,
        Jigawa,
        Kaduna,
        Kano,
        Kastina,
        Kebbi,
        Kogi,
        Kwara,
        Lagos,
        Nasarawa,
        Niger,
        Ogun,
        Ondo,
        Osun,
        Oyo,
        Plateau,
        Rivers,
        Sokoto,
        Taraba,
        Yobe,
        Zamfara
    }

    public static class States
    {
        public static State[] GetStatesInRegion(Region region)
        {
            switch (region)
            {
                case Region.NorthCentral:
                    return new[] {
                        State.Benue, State.Kogi, State.Kwara, State.Nasarawa, State.Niger, State.Plateau, State.Fct
                    };
                case Region.NorthEast:
                    return new[] {
                        State.Adamawa, State.Bauchi, State.Borno, State.Gombe, State.Taraba, State.Yobe
                    };
                case Region.NorthWest:
                    return new[] {
                        State.Jigawa, State.Kaduna, State.Kano, State.Kastina, State.Kebbi, State.Sokoto, State.Zamfara
                    };
                case Region.SouthEast:
                    return new[] {
                        State.Abia, State.Anambra, State.Ebonyi, State.Enugu, State.Imo
                    };
                case Region.SouthSouth:
                    return new[] {
                        State.AkwaIbom, State.Bayelsa, State.CrossRiver, State.Rivers, State.Delta, State.Edo
                    };
                case Region.SouthWest:
                    return new[] {
                        State.Ekiti, State.Lagos, State.Ogun, State.Ondo, State.Osun, State.Oyo
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(region), region, null);
            }
        }
    }
}