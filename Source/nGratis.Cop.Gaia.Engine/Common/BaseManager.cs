namespace nGratis.Cop.Gaia.Engine
{
    using nGratis.Cop.Core.Contract;

    public abstract class BaseManager : IManager
    {
        public bool IsInitialized
        {
            get;
            private set;
        }

        protected IGameInfrastructure GameInfrastructure
        {
            get;
            private set;
        }

        public void Initialize(IGameInfrastructure gameInfrastructure)
        {
            Guard.AgainstNullArgument(() => gameInfrastructure);

            this.GameInfrastructure = gameInfrastructure;
            this.InitializeCore();
            this.IsInitialized = true;
        }

        protected virtual void InitializeCore()
        {
        }
    }
}