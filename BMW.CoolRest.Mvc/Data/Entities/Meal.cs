namespace BMW.CoolRest.Mvc.Data.Entities
{
    public class Meal
    {
        public Meal()
        {
            Ingredients = new List<Ingredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
