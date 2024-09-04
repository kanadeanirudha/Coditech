namespace Coditech.Common.API.Model
{
    public class TreeViewModel
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        //public string selected { get; set; }
        public State state { get; set; }
    }

    public class State
    {
        public bool opened { get; set; } = true;
        public bool selected { get; set; } = true;
    }
}
