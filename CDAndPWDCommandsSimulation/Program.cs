public class CDAndPWDCommandsSimulation
{
    public void GetCommandResult(List<string> Commands)
    {
        string strCurrentDir = string.Empty;
        foreach (string strCommand in Commands)
        {
            if (strCommand.ToLower().StartsWith("pwd"))
            {
                Console.WriteLine("/" + strCurrentDir + "/");
                continue;
            }

            if(strCommand[3..].StartsWith("/"))
            {
                strCurrentDir = string.Empty;
            }
            strCurrentDir = GetCurrentDir(strCommand, strCurrentDir);
        }
    }

    private string GetCurrentDir(string strCommand, string strCurrentDir)
    {
        if(strCommand.ToLower().StartsWith("cd"))
        {
            List<string> SeparatedBySlash = strCommand[3..].Split("/").ToList();
            SeparatedBySlash.InsertRange(0, strCurrentDir.Split("/").ToList());
            SeparatedBySlash.RemoveAll(x => x == ".");
            return FindCurrentDir(SeparatedBySlash);
        }
        return string.Empty;
    }

    private string FindCurrentDir(List<string> SeparatedCollection)
    {
        List<string> CurrentDirs = new();
        foreach(string strSeparated in SeparatedCollection)
        {
            if(strSeparated == ".." && CurrentDirs.Any())
            {
                CurrentDirs.RemoveAt(CurrentDirs.Count - 1);
            }
            else if(!string.IsNullOrEmpty(strSeparated))
            {
                CurrentDirs.Add(strSeparated);
            }
        }
        return string.Join("/", CurrentDirs);
    }
}

public class Program
{
    public static void Main()
    {
        CDAndPWDCommandsSimulation CDAndPWDCommandsSimulation = new CDAndPWDCommandsSimulation();

        CDAndPWDCommandsSimulation.GetCommandResult(new List<string>()
        { 
            "cd /home", "cd user", "pwd", "cd ..", "pwd", "cd ./projects/../code", "pwd"
        });

        Console.WriteLine();

        CDAndPWDCommandsSimulation.GetCommandResult(new List<string>() 
        {
            "cd /", "cd home", "cd ./user//", "cd ../..", "cd ../..",
            "cd var/log","pwd", "cd /etc/./nginx/../ssh", "pwd", "cd ..", "pwd"
        });
    }
}