# ODB_l1 Schoolsearch
_Created for the course "Organization of databases" V. N. Karazin Kharkiv National University_

C# 12.0 .NET 8.0  console program for writing a text file database management DBMS.
___

__Objective__: Creating a system for searching for information from a file according to the appropriate criteria.
Task statement: Loading and processing the database from the student.txt file, searching for the necessary data and outputting it in the required format, implementing a system of commands to interact with the program. The programming language is C# using the Linq query language.

__Program model__:
The program consists of several classes:
- `Program` - the main class of the program, responsible for the user interface of interaction with the program, the logic of the program.
- `FileStorage` - responsible for the program's interaction with files, writing and reading files, checking for existence and permission.
- `ItemsList` - (DBMS) is responsible for storing Item objects, searching by appropriate criteria.
- `Item` - is responsible for the structure of each record (row) of the database.

The program interface is based on “commands” entered through the terminal; the database is created and deleted each time it is accessed.
Implemented commands:
- `-H[elp]` - displays a list of commands.
- `-A[uthor]` - displays the author of the program.
- `-Autoclosing: <true/false>` - if “false”, the program will wait for any key to be pressed after exiting, by default “true”.
- `-B[us]: <number>` - displays a list of objects in which the Bus field matches the data, in the format “StLastName StFirstName | Grade | Classroom”.
- `-C[lassroom]: <number>` - displays a list of objects in which the Classroom field matches the data, in the format “StLastName StFirstName”.
- `-F[ile]: <filePath>` - changes the database file to the specified one, by default “student.txt”.
- `-Saveresult: <true/false>` - if “true”, the program saves the search results in the file “result.csv” (CSV format) instead of outputting to the terminal, by default “false”.
- `-Skipline: <number>` - specifies how many lines should be skipped when reading a database file, it is necessary to skip the table header if there is one, the default is “0”.
- `-S[tudent]: <lastName>` - displays a list of objects in which the StLastName field matches the data, in the format “StLastName StFirstName | Grade | Classroom | TLastName TFirstName”.
- `-S[tudent]B[us]: <lastName>` - displays a list of objects in which the StLastName field matches the given field in the format “StLastName StFirstName | Bus”.
- `-T[eacher]: <lastname>` - displays a list of objects in which the TLastName field matches the given field in the format “StLastName StFirstName | TLastName TFirstName”.
- `-Q[uit]` - terminates the program.

The program supports the use of commands as startup arguments, for example:
`.\l1.exe -Autoclosing: false -Skipline: 3 -Saveresult: true -SB: CIGANEK`
If the program starts without arguments, it switches to interactive mode.

# Results of implementation:
![image](https://github.com/user-attachments/assets/e73f8d4d-018c-47c3-bcd8-25741ae83026)


