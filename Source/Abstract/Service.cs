namespace Core.Abstract
{
    public abstract class Service
    {
        public Service()
        {
            Game.Models.Populate(this);
            
            if (this is IUpdatable updatable)
            {
                Game.Life.OnUpdate.Listen(updatable.Update);
            }
        }
    }
}