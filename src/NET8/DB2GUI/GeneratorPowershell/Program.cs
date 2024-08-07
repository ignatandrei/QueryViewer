﻿using Spectre.Console;
using System.Diagnostics;
var wait = Environment.GetEnvironmentVariable("waitSeconds");
if(wait != null)
{ 
    if(int.TryParse(wait, out var waitSec))
    {
        AnsiConsole.MarkupLine($"[blue]Wait {waitSec} seconds[/]");
        await Task.Delay(waitSec*1000);
    }
} 
var pathGenerateFromDB = Environment.CurrentDirectory;
var directory = (pathGenerateFromDB);
if (directory.EndsWith(@"bin\Debug\net7.0"))
    directory = directory.Replace(@"bin\Debug\net7.0", "");
if (directory.EndsWith(@"bin\Debug\net7.0\"))
    directory = directory.Replace(@"bin\Debug\net7.0\", "");
if (directory.EndsWith(@"bin\Debug\net8.0"))
    directory = directory.Replace(@"bin\Debug\net8.0", "");
if (directory.EndsWith(@"bin\Debug\net8.0\"))
    directory = directory.Replace(@"bin\Debug\net8.0\", "");
AnsiConsole.MarkupLine("[blue]!!!Make sure you have edited connectiondetails.txt file ![/]");
AnsiConsole.MarkupLine($":play_button: Running in folder {pathGenerateFromDB}");
//Console.WriteLine(pathGenerateFromDB); 
string file = "connectionDetails.txt";
string text = await File.ReadAllTextAsync(file);
var root = System.Text.Json.JsonSerializer.Deserialize<Root>(text);
if (root == null) 
{
    Console.WriteLine($"do not have anything in {file}");
    return -3; 
}

int nrCon = 0; 
if(root?.connections != null )
foreach (var item in root.connections)
{
    nrCon++;
    var message = $"""
start connection {nrCon} {item.NameContext} at {DateTime.Now:hh:mm:ss}
""";

    AnsiConsole.MarkupLine($":person_running:[blue]{message}[/]");
    if (!item.Enabled)
    {
        message = $"""
skip runPowershell connection {nrCon} at {DateTime.Now:hh:mm:ss}
""";
        AnsiConsole.MarkupLine($"[blue]{message}[/]:left_arrow:");
        continue; 
    }
    var Provider = item.Provider ?? "";
    var ProjectForContext = item.ProjectForContext ?? "";
    var ProjectForClasses = item.ProjectForClasses ?? "";
    var ProjectWithDesigner = item.ProjectWithDesigner ?? "";
    var contextName = item.NameContext ?? "";
    var connection = item.ConnectionString ?? "";
        var projectCRA = item.ProjectCRA ?? "";
    if (projectCRA.Length* connection.Length * contextName.Length * Provider.Length * ProjectForClasses.Length * ProjectForContext.Length * ProjectWithDesigner.Length == 0)
    {
        Console.WriteLine($"missingData for connection {nrCon}");
        continue;
    }
    //Console.WriteLine(message);
    try
    {
        foreach (var errMessage in RunPowerShell(item, directory))
        {

                Console.WriteLine($"Error running Powershell:" +errMessage);
                //AnsiConsole.MarkupLine("[red]!!!ERROR:"+errMessage);

        }
    }
    catch(Exception ex)
    {
       Console.WriteLine($"[red]ERROR[/]:person_facepalming:");
    }

    var messageEnd = $"""
end runPowershell connection {nrCon} at {DateTime.Now:hh:mm:ss}
""";
    AnsiConsole.MarkupLine($"[blue]{messageEnd}[/]:person_raising_hand:");


}
AnsiConsole.MarkupLine(":eject_button: end generation");
return 0;


IEnumerable<string> RunPowerShell(Connection item, string directory)
{
    var file = Path.Combine(directory, "create.ps1");

    ProcessStartInfo startInfo = new() 
    {
        FileName = @"powershell.exe",
        WorkingDirectory = directory,
        
    };
    string arguments = @"-NoProfile -NonInteractive -ExecutionPolicy ByPass ";
    arguments += $"""
            -f "{file}"  
            """;
    //arguments += $" -connection {ConnectionString}";
    arguments += $" -provider {item.Provider}";
    arguments += $" -projectContext {item.ProjectForContext}";
    arguments += $" -projectModels {item.ProjectForClasses}";
    arguments += $" -projectWeb {item.ProjectWithDesigner}";
    arguments += $" -nameContext {item.NameContext}";
    arguments += $" -connection \"{item.ConnectionString}\"";
    arguments += $" -ProjectCRA \"{item.ProjectCRA}\"";

    startInfo.Arguments = arguments;
    Console.WriteLine("powershell.exe "+arguments);
    startInfo.RedirectStandardOutput = true;
    startInfo.RedirectStandardError = true;
    startInfo.UseShellExecute = false;
    startInfo.CreateNoWindow = true;
    Process process = new()
    {
        StartInfo = startInfo
    };
    string output = "", errors = "";
    process.OutputDataReceived += (sender, e) => { if (e.Data != null) output += e.Data + Environment.NewLine; };
    process.ErrorDataReceived += (sender, e) => { if (e.Data != null) errors += e.Data + Environment.NewLine; };

    process.Start();
    process.PriorityClass= ProcessPriorityClass.RealTime;
    process.BeginOutputReadLine();
    process.BeginErrorReadLine();
    process.WaitForExit();

    
    if (errors.Length > 0 || output.Contains("SqlException") || output.Contains("Build failed"))
    {
        //yield return "To reproduce please run powershell " + arguments;
        var tempFile = Path.GetTempFileName() + ".txt";
        if(errors.Length>0)
            yield return errors;

        File.WriteAllText(tempFile, output + errors);
        //Process.Start("notepad.exe", tempFile);
        var message = tempFile;
        yield return tempFile;
    }
}