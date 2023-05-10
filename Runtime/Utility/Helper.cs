namespace Kadinche.Kassets
{
    public static class MenuHelper
    {
        public const string DefaultCommandMenu = "Kassets/Commands/";
        public const string DefaultGameEventMenu = "Kassets/Game Events/";
        public const string DefaultVariableMenu = "Kassets/Variables/";
        public const string DefaultCollectionMenu = "Kassets/Collections/";
        public const string DefaultRequestResponseEventMenu = "Kassets/Request Response Events/";
        public const string DefaultObjectPoolMenu = "Kassets/Object Pools/";
        public const string DefaultOtherMenu = "Kassets/Others/";
    }
    
    public enum VariableEventType
    {
        ValueAssign,
        ValueChange
    }
}