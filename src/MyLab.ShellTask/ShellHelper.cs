using System.Diagnostics;
using MyLab.Log.Dsl;

namespace MyLab.ShellTask;

public static class ShellHelper
{
    public static Task<int> Bash(string cmd, IDslLogger? logger)
    {
        var source = new TaskCompletionSource<int>();
        var escapedArgs = cmd.Replace("\"", "\\\"");
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = $"-c \"{escapedArgs}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            },
            EnableRaisingEvents = true
        };
        process.Exited += (sender, args) =>
        {
            if (logger != null)
            {
                var procError = process.StandardError.ReadToEnd();
                var procOut = process.StandardOutput.ReadToEnd();

                var logMsg = string.IsNullOrWhiteSpace(procError)
                    ? logger.Error("Script execution error")
                        .AndFactIs("stderr", procError)
                    : logger.Action("Script execution completed");

                logMsg
                    .AndFactIs("stdout", procOut)
                    .AndFactIs("exit-code", process.ExitCode)
                    .Write();
            }


            if (process.ExitCode == 0)
            {
                source.SetResult(0);
            }
            else
            {
                source.SetException(new Exception($"Script execution failed with exit code `{process.ExitCode}`"));
            }

            process.Dispose();
        };

        try
        {
            process.Start();
        }
        catch (Exception e)
        {
            logger.Error("Script execution failed", e).Write();
            source.SetException(e);
        }

        return source.Task;
    }
}