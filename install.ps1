# Pathogen.vim Installer for Windows
# (C) Retorillo / The MIT license

$remote = "https://tpo.pe/pathogen.vim"
$local  = "$home\vimfiles\autoload\pathogen.vim"
$ErrorActionPreference = "Stop"

"vimfiles", "vimfiles\autoload", "vimfiles\bundle" | % {
	if (-not (Test-Path "$home\$_")) {
		[void](mkdir "$home\$_")
		echo "$_ is created"
	}
	else {
		echo "$_ exists"
	}
}

if (-not (Test-Path $local)) {
	$client = New-Object Net.WebClient
	$client.DownloadFile($remote, $local)
	$client.Dispose()
	echo "pathogen.vim is downloaded"
}
else {
	echo "pathogen.vim exists"
}

vim --noplugin -n -e -s -c "verbose source `"$PSScriptRoot\patch.vim`"" +q
echo ""
