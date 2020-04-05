namespace Core.Abstract
{
    public abstract class Service
    {
        protected Service()
        {
            Game.Models.Populate(this);
            
            if (this is IUpdate iupdate)
            {
                Game.Life.OnUpdate.Listen(iupdate.Update);
            }
        }

        protected internal virtual void Reset() { }
    }
}