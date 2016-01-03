using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Collections.Generic;

class InstallPathgenVim {
	const string PathogenVimUrl = "https://tpo.pe/pathogen.vim";
	static readonly string Home = Environment.GetEnvironmentVariable("USERPROFILE");
	static readonly Stack<string> CdStack = new Stack<string>(8);
	static readonly string[] VimRcAppendLines = new string[]{
		"execute pathogen#infect()"
	};
	static bool Exec(string label, Predicate<string> pred, Action<string> act, string arg) {
		var p = pred(arg);
		Console.WriteLine("[{1}{0}] {2}", p ? "+" : "=", label, arg);
		if (p) act(arg);
		return p;
	}
	static string Cd {
		get {
			return Environment.CurrentDirectory;
		}
		set {
			Environment.CurrentDirectory = value;
			Console.WriteLine("[CD] {0}", value == Home ? "~\\" : value.Replace(Home, "~"));
		}
	}
	static void Pushd(string d) {
		CdStack.Push(Cd);
		Cd = d;
	}
	static bool Popd() {
		if (CdStack.Count() == 0) return false;
		Cd = CdStack.Pop();
		return true;
	}
	static void Main(){
		Predicate<string> pred_fnf  = f => { return !File.Exists(f); };
		Predicate<string> pred_dnf  = d => { return !Directory.Exists(d); };
		Action<string>    act_mkdir = d => { Directory.CreateDirectory(d); };
		try {
			Pushd(Home);
			var utf8n = new UTF8Encoding();
			var vimrc = ".vimrc";
			var _vimrc = "_vimrc";
			if (File.Exists(_vimrc))
				vimrc = _vimrc;
			foreach (var append in VimRcAppendLines) {
				Exec("VIMRC", a => {
					return File.ReadAllLines(vimrc).All(l => string.Compare(l, a, true) != 0);
				}, a => {
					File.AppendAllLines(vimrc, new string[] { a }, utf8n);
				}, append);
			}
			
			var vimfiles = "vimfiles";
			Exec("MKDIR", pred_dnf, act_mkdir, vimfiles);
			Pushd(vimfiles);
			(new string [] { "autoload", "bundle" }).All(d => {
				Exec("MKDIR", pred_dnf, act_mkdir, d);
				return true;
			});
			var pathogenvim = "autoload/pathogen.vim";
			Exec("DL", pred_fnf, (url) => {
				using (var client = new WebClient())
					client.DownloadFile(PathogenVimUrl, pathogenvim);
			}, pathogenvim);
			
		}
		finally {
			while (Popd());
		}
		Console.WriteLine("Pathogen.vim is installed");
	}
}
