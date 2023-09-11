# Icod.Distinct
Distinct.exe returns only unique lines of text from input.

## Usage
`Distinct.exe (-h | --help | /help)`
Displays this text.

`Distinct.exe (-c | --copyright | /copyright)`
Displays copyright and licensing information.

`Distinct.exe [(-i | --input | /input) inputFilePathName] [(-o | --output | /output) outputFilePathName] [(-ic | --ignoreCase | /ignoreCase) (true|false)] [(-n | --name | /name) cultureName]`
Distinct.exe returns only unique lines of text from input.
inputFilePathName and outputFilePathName may be relative or absolute paths.
If inputFilePathName is omitted then input is read from StdIn.
If outputFilePathName is omitted then output is written to StdOut.
The name switch specifies the name of a culture, which is not case-sensitive.
The ignoreCase switch should be true to specify that comparison operations be case-insensitive; false to specify that comparison operations be case-sensitive.

## Copyright and Licensing
Distinct.exe returns only unique lines of text from input.
Copyright( C ) 2023 Timothy J. Bruce

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published 
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
