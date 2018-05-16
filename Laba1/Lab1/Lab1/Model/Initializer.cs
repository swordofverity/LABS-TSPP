using System.Data.Entity;

namespace Lab1.Model
{
    class Initializer : CreateDatabaseIfNotExists<EventContext>
    {
        protected override void Seed(EventContext context)
        {
            //context.Venues.Add(new Venue { Capacity = 1000, Map = new byte[] { 0 }, Name = "test", Remarks = "test" });
            //context.SaveChanges();
            base.Seed(context);
        }
    }
}
