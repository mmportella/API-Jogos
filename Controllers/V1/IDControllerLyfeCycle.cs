using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace API_Jogos.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IDControllerLyfeCycle : ControllerBase
    {
        public readonly ISingletonExample _singletonExample1;
        public readonly ISingletonExample _singletonExample2;

        public readonly IScopedExample _scopedExample1;
        public readonly IScopedExample _scopedExample2;

        public readonly ITransientExample _transientExample1;
        public readonly ITransientExample _transientExample2;

        public IDControllerLyfeCycle(ISingletonExample exemploSingleton1,
                                     ISingletonExample exemploSingleton2,
                                     IScopedExample exemploScoped1,
                                     IScopedExample exemploScoped2,
                                       ITransientExample exemploTransient1,
                                       ITransientExample exemploTransient2)
        {
            _singletonExample1 = exemploSingleton1;
            _singletonExample2 = exemploSingleton2;
            _scopedExample1 = exemploScoped1;
            _scopedExample2 = exemploScoped2;
            _transientExample1 = exemploTransient1;
            _transientExample2 = exemploTransient2;
        }

        [HttpGet]
        public Task<string> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Singleton 1: {_singletonExample1.gameId}");
            stringBuilder.AppendLine($"Singleton 2: {_singletonExample2.gameId}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Scoped 1: {_scopedExample1.gameId}");
            stringBuilder.AppendLine($"Scoped 2: {_scopedExample2.gameId}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Transient 1: {_transientExample1.gameId}");
            stringBuilder.AppendLine($"Transient 2: {_transientExample2.gameId}");

            return Task.FromResult(stringBuilder.ToString());
        }

    }

    public interface IGeneralExample
    {
        public Guid gameId { get; }
    }

    public interface ISingletonExample : IGeneralExample
    { }

    public interface IScopedExample : IGeneralExample
    { }

    public interface ITransientExample : IGeneralExample
    { }

    public class LifeCycleExample : ISingletonExample, IScopedExample, ITransientExample
    {
        private readonly Guid _guid;

        public LifeCycleExample()
        {
            _guid = Guid.NewGuid();
        }

        public Guid gameId => _guid;
    }
}