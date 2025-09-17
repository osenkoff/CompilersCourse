program Reverse;
var
  line: string;
  i, length_line: integer;
  reversed_line: string;
begin
  Write('Введите строку: ');
  ReadLn(line);
  
  length_line := Length(line);
  reversed_line := '';
  
  for i := length_line downto 1 do
    reversed_line := reversed_line + line[i];
    
  WriteLn('Перевернутая строка: ', reversed_line);  
end.