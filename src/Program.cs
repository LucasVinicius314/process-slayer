using System.Data;
using System.Diagnostics;

class Program
{
  static void Main(string[] args)
  {
    var processesToKill = new string[]{
      "OVRRedir",
      "OVRServer_x64",
      "OVRServiceLauncher",
      "PnkBstrA",
    };

    var processMap = new Dictionary<string, List<Process>>();

    foreach (var process in Process.GetProcesses())
    {
      if (!processMap.ContainsKey(process.ProcessName))
      {
        processMap[process.ProcessName] = new List<Process>();
      }

      processMap[process.ProcessName].Add(process);
    }


    Console.WriteLine($"Current running processes ({processMap.Keys.Count}):");
    foreach (var processName in processMap.Keys.OrderBy(x => x).ToArray())
    {
      Console.WriteLine($"  - {processName}");
    }

    var killedProcesses = new List<string>();

    foreach (var entry in processMap)
    {
      if (processesToKill.Contains(entry.Key))
      {
        foreach (var process in entry.Value)
        {
          process.Kill();

          killedProcesses.Add(entry.Key);
        }
      }
    }

    Console.WriteLine($"Killed processes ({killedProcesses.Count}):");
    foreach (var processName in killedProcesses)
    {
      Console.WriteLine($"  - {processName}");
    }
  }
}
