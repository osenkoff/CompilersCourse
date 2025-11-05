program FactorialCalculator;
var
  number: integer;
  result_fact: integer;

function calculateFactorial(n: integer):integer;
begin
  if n < 0 then 
    calculateFactorial := -1
  else if ((n = 0) or (n = 1)) then 
    calculateFactorial := 1
  else 
    calculateFactorial := n * calculateFactorial(n - 1);
end;

begin
  Write('Введите неотрицательное число: ');
  ReadLn(number);
  
  result_fact := calculateFactorial(number);

  if result_fact = -1 then 
    WriteLn('Факториал не определяется для отрицательных чисел')
  else
    WriteLn('Результат: ', result_fact);
end.
