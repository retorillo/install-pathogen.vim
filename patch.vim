" Add required lines to vimrc for pathogen.vim
" (C) Retorillo / The MIT license

let s:candidates = [ "~/_vimrc", "~/.vimrc" ]
let s:lines      = [ "execute pathogen#infect()" ]

function! s:patch()
	let l:affected = s:patchCandidates(s:candidates, s:lines)
	if l:affected == -1
		echoerr "Cannnot append lines into your vimrc"
		return
	endif
	echo l:affected . " lines are appended into your vimrc"
endfunction

function! s:trimLeft(str)
	return substitute(a:str, "^\\s*", "", "")
endfunction

function! s:trimRight(str)
	return substitute(a:str, "\\s*$", "", "")
endfunction

function! s:trim(str)
	return s:trimLeft(s:trimRight(a:str))
endfunction

function! s:getLast(list)
	return a:list[len(a:list)-1]
endfunction

function! s:containsLine(list, line)
	for l:l in a:list
		if s:trim(l:l) == s:trim(a:line) 
			return 1
		endif
	endfor
	return 0
endfunction

function! s:patchLines(dlines, slines)
	let l:count = 0
	for l:l in a:slines
		if !s:containsLine(a:dlines, l:l)
			call add(a:dlines, l:l) 
			let l:count += 1
		endif
	endfor
	return l:count
endfunction

function! s:patchFile(dfile, slines)
	if !filereadable(a:dfile)
		return -1
	endif
	let l:dlines   = readfile(a:dfile)
	let l:affected = s:patchLines(l:dlines, a:slines)
	if writefile(l:dlines, a:dfile) != -1
		return l:affected
	else
		return -1
	endif
endfunction

function! s:patchCandidates(cfiles, lines)
	for l:cfile in a:cfiles
		let l:affected = s:patchFile(expand(l:cfile), a:lines)
		if l:affected != -1
			return l:affected
		endif
	endfor
	return -1
endfunction

call s:patch()

