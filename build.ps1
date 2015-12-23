$f = "$PSScriptRoot\install"
if (test-path "$f.exe") { del "$f.exe" }
csc /out:"$f.exe" "$f.cs" 
if (test-path "$f.exe") { &"$f.exe" }

