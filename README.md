# BoggleConsole
This is a console version of the Boggle game. It uses a simple dictionary stored in `Dictionary.txt` containing the word count on the first line followed by a sorted list of words, one word per line.

To run:
1. Open PowerShell in the current directory
2. Build the project (`dotnet build`)
3. Copy the dictionary to the application directory (`copy .\Dictionary.txt .\BoggleConsole\bin\Debug\`)
4. Move to the appilcation directory (`cd .\BoggleConsole\bin\Debug`)
5. Type `.\BoggleConsole.exe` to run, use the `-?` switch for usage:


```
Usage: [-?|-help][-Seed seed][-Game number][-File filename][-WordSize min][-Pause][-Verbose][-Step]
-help      Display this usage message
-Seed      int, seed the random number generator with seed
-Game      The number of the game to play, see below
-File      Load the board from the file specified by filename
-WordSize  int, set the minimum word size to min
-Pause     Pause before showing the solution
-Verbose   Show output for generating and solving the game
-Step      Pause when checking each letter

Boggle games:
1: Boggle Classic
2: Boggle New
3: Boggle German
4: Boggle French
5: Boggle Dutch
6: Big Boggle Classic
7: Big Boggle Challenge
8: Big Boggle Deluxe
9: Big Boggle 2012
10: Super Big Boggle

If Game and File aren't specified, Boggle Classic is played. The board file format uses spaces to delineate letters, multiple lines are okay, Q represents Qu and these numbers represent letter combinations: 0 = Blank, 1=Qu, 2=In, 3=Th, 4=Er, 5=He, 6=An
```
