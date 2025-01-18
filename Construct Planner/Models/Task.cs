namespace Construct_Planner.Models {
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int ObraId { get; set; }
        public string Color { get; set; }
    }
}