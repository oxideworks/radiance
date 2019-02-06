def zip_with(fn, xs, ys):
    return [fn(a, b) for (a, b) in zip(xs, ys)]

def loop(xs):
    return xs + [xs[0]]