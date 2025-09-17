try:
    number = float(input('Введите действительное число: '))
    if (number < 0):
        print('ERROR')
    else:
       result = number ** 0.5
       print(f'Квадратный корень = {result}')
except ValueError:
    print('ERROR')