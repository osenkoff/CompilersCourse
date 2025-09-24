program SquareRoot;
var
  number: real;
  result_num: real;

begin
  Write('Введите действительное число: ');
  ReadLn(number);
  
  if (number < 0) then
    WriteLn('ERROR')
  else
    begin
      result_num := Sqrt(number);
      WriteLn('Квадратный корень = ', result_num);
    end;
end.