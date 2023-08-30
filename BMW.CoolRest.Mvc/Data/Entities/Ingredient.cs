namespace BMW.CoolRest.Mvc.Data.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public List<Meal> Meals { get; set; }
    }
}
