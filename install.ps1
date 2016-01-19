# Pathogen.vim Installer for Windows
# (C) Retorillo / The MIT license

$remote = "https://tpo.pe/pathogen.vim"
$local  = "$home\autoload\pathogen.vim"
$ErrorActionPreference = "Stop"

"autoload", "bundle" | % {
	if (Test-Path "$home\$_") {
		[void](mkdir "$home\$_")
		echo "$_ is created"
	}
}

if (Test-Path $local) {
	$client = New-Object Net.WebClient
	$client.DownloadFile($remote, $local)
	$client.Dispose()
	echo "$remote is downloaded"
}

vim --noplugin -n -e -s -c "verbose source `"$PSScriptRoot\patch.vim`"" +q
echo ""
