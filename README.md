# Number Of Any Base

This is a struct that can represent an integer value of any base (i.e. Binary, Octal, Decimal, Hexadecimal).

## Known Limitations
1. It only supports up to Base-62 out of the box. This support is limited by the OptionAllNumberValues string property’s length. (Adding more possible values increases the number of supported bases, but 62 seemed like plenty for most use-cases.)
1. Does not support any decimal places. It’s essentially an integer for other bases.
1. Does not support math results that are less than Int32.MinInt, or greater than Int32.MaxInt, since mathematical operators (+,-,*,/,%) use Int32 methods. Operations that exceed these limits will throw an OverflowException.

## Declaration
* `Number n = "10|3";`
* `var n = new Number("10|3");`
* `Number n = 3;` (uses **OptionDefaultBaseForStringParsing**)
* `Number n = "3";` (uses **OptionDefaultBaseForStringParsing**)
* `var n = new Number(3);` (uses **OptionDefaultBaseForStringParsing**)
* `var n = new Number("3");` (uses **OptionDefaultBaseForStringParsing**)

## Configuration
* **OptionAllNumberValues : string**
    * Default: A string of integers, uppercase, and lowercase letters.
* **OptionDelimiter : char**
    * Default: |
* **OptionDefaultBaseForStringParsing : int**
    * Default: 10
* **OptionUseDecimalBaseForIntLiterals : bool**
    * Default: true

## Usage Examples
### Math
``` 
Number a = "2|1";
Number b = "2|1";
Console.WriteLine(a + b); // Output (in Base-2): 10
```
``` 
Number a = "2|100";
Number b = "2|1";
Console.WriteLine(a - b); // Output (in Base-2): 11
```
``` 
Number a = "2|11";
Number b = "2|11";
Console.WriteLine(a * b); // Output (in Base-2): 1001
```
``` 
Number a = "2|110";
Number b = "2|11";
Console.WriteLine(a / b); // Output (in Base-2): 10
```
``` 
Number a = "2|111";
Number b = "2|110";
Console.WriteLine(a % b); // Output (in Base-2): 1
```
### Literal Assignment
#### Binary
```
Number.OptionDefaultBaseForStringParsing = 2;
Number.OptionUseDecimalBaseForIntLiterals = false;
Number n = 01101101;
Console.WriteLine(n.BaseValue); // Output: 2
```
#### Octal
```
Number.OptionDefaultBaseForStringParsing = 8;
Number.OptionUseDecimalBaseForIntLiterals = false;
Number n = 01234567;
Console.WriteLine(n.BaseValue); // Output: 8
```
#### Hexadecimal
```
Number.OptionDefaultBaseForStringParsing = 16;
Number n = "0123456789ABCDE";
Console.WriteLine(n.BaseValue); // Output: 16
```
