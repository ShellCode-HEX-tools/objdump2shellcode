# objdump2shellcode
A very simple tool that dumps shellcode from a provided binary. This tools is useful when encoding and creating custom shellcode as it includes bad character detection. Below is an example of dumping in nasm format, making copying/pasting into an encoder an ease:

![alt text](https://raw.githubusercontent.com/wetw0rk/objdump2shellcode/master/pictures/nasm_output.png)

We can also change the variable name, this can come in handy so that you dont have to change ever variable in your exploit:

![alt text](https://raw.githubusercontent.com/wetw0rk/objdump2shellcode/master/pictures/panda.png)

Of course we can also do a comment dump. Handy for submissions:

![alt text](https://raw.githubusercontent.com/wetw0rk/objdump2shellcode/master/pictures/c_dump.png)
