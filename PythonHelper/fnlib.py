def zip_with(fn, xs, ys):
    return [fn(a, b) for (a, b) in zip(xs, ys)]