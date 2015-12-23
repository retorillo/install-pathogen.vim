using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

class InstallPathgenVim {
	const string PathogenVimUrl = "https://tpo.pe/pathogen.vim";
	static readonly string[] VimRcAppendLines = new string[]{
		"execute pathogen#infect()"
	};

	static void Main(){
		var home = Environment.GetEnvironmentVariable("USERPROFILE");
		var vimfiles = Path.Combine(home, "vimfiles");
		var pwd = Environment.CurrentDirectory;
		Environment.CurrentDirectory = vimfiles;
		try {
			(new string [] { "autoload", "bundle" }).All(d => {
				Console.WriteLine(string.Format("[MKDIR] {0}", d));
				if (!Directory.Exists(d))
					Directory.CreateDirectory(d);
				return true;
			});

			Console.WriteLine("[DOWNLOAD] pathgen.vim");
			var pathogenvim = "autoload/pathogen.vim";
			if (!File.Exists(pathogenvim))
				using (var client = new WebClient())
					client.DownloadFile(PathogenVimUrl,pathogenvim);
			
			Console.WriteLine("[APPEND] vimrc");
			var utf8n = new UTF8Encoding();
			var vimrc = "../.vimrc";
			foreach (var append in VimRcAppendLines) {
				if (File.ReadAllLines(vimrc).All(line => string.Compare(line, append, true) != 0))
					File.AppendAllLines(vimrc, new string[] { append }, utf8n);
			}
		}
		finally {
			Environment.CurrentDirectory = pwd;	
		}
	}
}
