using System.Diagnostics;

Console.WriteLine("Start running project");
var p=new Process();
p.StartInfo.FileName = "dotnet.exe";
p.StartInfo.Arguments = "tool update --global dotnet-ef";
p.Start();
p.WaitForExit();



