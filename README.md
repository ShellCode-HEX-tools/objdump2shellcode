# objdump2shellcode

objdump2shellcode is a very simple tool that dumps shellcode from a provided binary. Normally when generating shellcode we will be dealing with a varying amount of opcodes -> instructions, making custom shellcoding a bit of a hassle when debugging issues. 

objdump2shellcode can aid in the following

  - identifying instructions resulting in bad opcodes (aka bad characters)
  - format output in various languages (python, c, powershell, javascript, etc)
  - accept shellcode via stdin and format it / detect bad characters
  - commented dump for shellcode submissions etc
  - custom encoding (nasm dump; think xor encoding etc)

### Example of bad character identification
![alt text](https://raw.githubusercontent.com/wetw0rk/objdump2shellcode/master/pictures/c_dump.png)

### If you use BlackArch Linux, objdump2shellcode comes preinstalled
![alt text](https://raw.githubusercontent.com/wetw0rk/objdump2shellcode/master/pictures/abatchy_shellcode.PNG)

You can download BlackArch here :smiley: : https://blackarch.org/

### What objdump2shellcode will not do

  - generate shellcode from nothing (you must have a binary to extract opcodes from)
  - generate a standalone exe / elf binary (may add this feature at a later date)

### Installation

objdump2shellcode requires [objdump](https://sourceware.org/binutils/docs/binutils/objdump.html) to run properly, and 99% of linux distro's have it installed by default. However I recommend a penetration testing distro such as Black Arch, or Kali Linux. However, if you compile your assembly instructions in NASM objdump is not necessary! Simply run --raw-dump or -rb and your shellcode will be extracted and formatted :wink:.

For ease of access I recommend adding it to the /usr/bin/ directory like so:

```sh
root@wetw0rk:~# chmod +x objdump2shellcode.py
root@wetw0rk:~# cp objdump2shellcode.py /usr/bin/objdump2shellcode
root@wetw0rk:~# objdump2shellcode
usage: objdump2shellcode [-h] [-d DUMP] [-rd RAW_DUMP] [-s] [-f FORMAT]
                         [-b BADCHAR] [-c] [-v VARNAME] [-l]

optional arguments:
  -h, --help            show this help message and exit
  -d DUMP, --dump DUMP  binary to use for shellcode extraction (via objdump)
  -rd RAW_DUMP, --raw-dump RAW_DUMP
                        dump the shellcode from provided file without using
                        objdump
  -s, --stdin           read ops from stdin (EX: echo -ne "\xde\xad\xbe\xef" |
                        objdump2shellcode -s -f python -b "\xbe")
  -f FORMAT, --format FORMAT
                        output format (use --list for a list)
  -b BADCHAR, --badchar BADCHAR
                        seperate badchars like so "\x00,\x0a"
  -c, --comment         comments the shellcode output
  -v VARNAME, --varname VARNAME
                        alternative variable name
  -l, --list            list all available formats
```

License
----

MIT


**Free Software, Hell Yeah!**

