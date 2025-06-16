namespace DetectiveAI.Story
{
    public class Investigation
    {
        public string Title { get; set; }
        public string Rol { get; set; }
        public string Intro { get; set; }
        public Details Details { get; set; }
        public string Oplossing { get; set; }
    }

    public class Details
    {
        public List<Character> Characters { get; set; } = new List<Character>();
        public List<string> Clues { get; set; } = new List<string>();
        public string Oplossing { get; set; } 

    }

    public class Character
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }

}