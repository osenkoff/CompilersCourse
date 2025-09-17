def calculate_factorial(number):
    if number < 0:
        return -1
    if number == 0 or number == 1:
        return 1
    return number * calculate_factorial(number - 1)

try:
    number = int(input('Введите неотрицательное число: '))
    result = calculate_factorial(number)
    if result == -1:
        print("Факториал не определяется для отрицательных чисел")
    else:
        print(f'Результат: {result}')
except ValueError:
    print('Ошибка: введите целое число!')
