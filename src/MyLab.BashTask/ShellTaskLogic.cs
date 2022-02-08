using MyLab.Log.Dsl;
using MyLab.TaskApp;

namespace MyLab.BashTask
{
    public class ShellTaskLogic : ITaskLogic
    {
        private readonly IDslLogger _log;

        /// <summary>
        /// Initializes a new instance of <see cref="ShellTaskLogic"/>
        /// </summary>
        public ShellTaskLogic(ILogger<ShellTaskLogic> logger)
        {
            _log = logger.Dsl();
        }

        public Task Perform(CancellationToken cancellationToken)
        {
            return ShellHelper.Bash("/script.sh", _log);
        }
    }
}
