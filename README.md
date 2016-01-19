# Pathogen.vim Installer for Windows

Installer for [pathogen.vim](https://github.com/tpope/vim-pathogen/)

Run `install.ps1` to install pathogen.vim

```PowerShell
PS> .\install.ps1
```

This script will DO:

- Make `~/vimfiles/bundle` and `~/vimfiles/autoload` if does not exist
- Download `~/vimfiles/autoload/pathogen.vim` if does not exist
- Add `execute pathogen#infect()` to your `_vimrc` or `.vimrc` if does not exist

Copyright (C) Retorillo / Distributed under the MIT License
