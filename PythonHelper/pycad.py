import sympy as sy
import numpy as np


def direct_average(data):
    return sum(data) / len(data)

def validify_data(data, sig3):
    removed = list()
    av = direct_average(data)
    i = 0
    while i < len(data):
        if abs(data[i] - av) > sig3:
            removed.append(data.pop(i))
            i -= 1
        i += 1
    return (data, removed)

def calc_3sig(measurements):
    dms = list()
    av = direct_average(measurements)
    for i in measurements:
        dms.append(i - av)
    sumsq = 0
    for i in dms:
        sumsq += i**2
    sig = (sumsq / (len(measurements) - 1))**0.5
    return 3 * sig

def get_student_coef(count):
    # 95%

    if count < 2:
        return 1
    # coeffs from 2 to 10
    coeffs1_10 = [12.7, 4.3, 3.2, 2.8, 2.6, 2.4, 2.4, 2.3, 2.3]
    if 2 <= count <= 10:
        return coeffs1_10[count - 2]

    # coeffs 15 20 40 60
    coeffs15_20_40_60 = [2.1, 2.1, 2.0, 2.0]
    if count <= 15:
        return coeffs15_20_40_60[0]
    if count <= 20:
        return coeffs15_20_40_60[1]
    if count <= 40:
        return coeffs15_20_40_60[2]
    if count <= 60:
        return coeffs15_20_40_60[3]


def direct_delta(validData, toolErr):
    dms = list()
    av = direct_average(validData)
    for i in validData:
        dms.append(i - av)
    sumsq = 0
    for i in dms:
        sumsq += i**2
    dstd = (sumsq / len(validData) / (len(validData) - 1))**0.5
    drnd = get_student_coef(len(validData)) * dstd
    return (drnd**2 + (toolErr / 2)**2)**0.5


def indirect_average(formula, averages):
    x, y, z = sy.symbols('x y z')
    sz = len(averages)
    if sz == 1:
        res = sy.lambdify(x, sy.sympify(formula))(averages[0])
    elif sz == 2:
        res = sy.lambdify((x, y), sy.sympify(formula))(averages[0], averages[1])
    elif sz == 3:
        res = sy.lambdify((x, y, z), sy.sympify(formula))(averages[0], averages[1], averages[2])
    else:
        print('No suppored size of varList')
    return res


def indirect_delta(formula, averages, deltas):
    if len(averages) != len(deltas):
        print('Error in indirect_delta')

    x, y, z = sy.symbols('x y z')
    formula = sy.sympify(formula)

    sz = len(averages)

    if sz == 1:
        res = sy.lambdify(x,
                          sy.sqrt((sy.diff(formula, x) * deltas[0])**2))(averages[0])
    elif sz == 2:
        res = sy.lambdify((x, y),
                          sy.sqrt((sy.diff(formula, x) * deltas[0])**2 +
                                  (sy.diff(formula, y) * deltas[1])**2))(averages[0], averages[1])
    elif sz == 3:
        res = sy.lambdify((x, y, z),
                          sy.sqrt((sy.diff(formula, x) * deltas[0])**2 +
                                  (sy.diff(formula, y) * deltas[1])**2 +
                                  (sy.diff(formula, z) * deltas[2])**2))(averages[0], averages[1], averages[2])
    else:
        print('No suppored size of varList')

    return res


def ols_value(x_vals, y_vals):
    x = np.array(x_vals)
    y = np.array(y_vals)
    A = np.vstack([x, np.ones(len(x))]).T
    k, b = np.linalg.lstsq(A, y, rcond=None)[0]
    return (k, b)  # y = kx + b

def ols_delta(x_vals, y_vals):
    k, b = ols_value(x_vals, y_vals)
    dbsq = 1 / sum(x_vals) / (len(x_vals) - 1) * sum([(i[1] - k * i[0])**2 for i in zip(x_vals, y_vals)])
    db = dbsq**0.5
    return db
