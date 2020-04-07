namespace Core.Abstract
{
    public abstract class Service
    {
        protected Service()
        {
            Game.Models.Populate(this);
            
            if (this is IUpdateHandler iupdate)
            {
                Game.Life.OnUpdate.Listen(iupdate.Update);
            }
        }

        protected internal virtual void Reset() { }
    }
}